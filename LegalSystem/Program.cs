using System.Text;
using LegalSystem.Application.Interfaces;
using LegalSystem.Application.Interfaces.Dbcontex;
using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Application.Mapping;
using LegalSystem.Application.Services;
using LegalSystem.Domain;
using LegalSystem.Infraestructura.Data;
using LegalSystem.Infraestructura.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
// cargar variables de entorno
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

////leer variables de entorno necesaria para configuracion
var host = Environment.GetEnvironmentVariable("HOST");
var port = Environment.GetEnvironmentVariable("PORT");
var database = Environment.GetEnvironmentVariable("DATABASE");
var user = Environment.GetEnvironmentVariable("USER");
var password = Environment.GetEnvironmentVariable("PASSWORD");

// construir la cadena de conexion 
var connectionString =
    $"Host={host};" +
    $"Port={port};" +
    $"Database={database};" +
    $"Username={user};" +
    $"Password={password};" +
    $"SSL Mode=Require;" +
    $"Trust Server Certificate=true;";



///Regristar el contexto de la base de datos en la conexion
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(connectionString));

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    // Configuraciones de contraseñas sencillas
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

//Registrar repositorio con sus interfaces
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICasoJuridicoRepository, CasoJuridicoRepository>();
builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

//Registrar  los servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICasoService, CasoService>();
builder.Services.AddScoped<ICitaService, CitaService>();
//registar el  contextAcseso
builder.Services.AddHttpContextAccessor();

//Registrar AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 1. Agregar los servicios al contenedor
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();

// CONFIGURACIÓN DE JWT 
var key = Encoding.UTF8.GetBytes("EstaEsMiLlaveSuperSecretaYMuyLarga123456789");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    config.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var mensaje = new { error = "Acceso denegado. Debe iniciar sesión como abogado para realizar esta acción." };
            return context.Response.WriteAsJsonAsync(mensaje);
        }
    };
});



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "LegalSystem API", Version = "v1" });

    // Configuración del esquema de seguridad
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, 
        Scheme = "bearer",             
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Simplemente pega tu token JWT aquí (sin la palabra Bearer)."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 1. Configuración de CORS
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // En desarrollo, permitimos localhost (Angular, React, etc.)
            policy.WithOrigins("http://localhost:4200", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
        else
        {
            // En producción (Render), puedes usar las URLs configuradas o permitir todo
            // solo si es necesario para pruebas rápidas:
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
    });
});

// 1. Construir la aplicación
var app = builder.Build();

// 2. Swagger siempre va primero para que esté disponible
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LegalSystem API v1");
});

// 3. Redirección a Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});


app.UseCors("FrontendPolicy");

//soporte de la autenticacion
app.UseAuthentication();      
app.UseAuthorization();        

// 4. Middlewares de manejo de errores
app.UseMiddleware<LegalSystem.Api.Middleware.ExceptionMiddleware>();

// 5. Mapeo de controladores y ejecución
app.MapControllers();

    var apiPort = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    app.Run($"http://0.0.0.0:{apiPort}");

