using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LegalSystem.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continúa con el flujo normal de la aplicación
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si ocurre un error en cualquier servicio, lo atrapamos aquí
                await ManejarExceptionAsync(context, ex);
            }
        }

        private static Task ManejarExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Por defecto, asumimos que es un error interno del servidor (500)
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocurrió un error inesperado en el servidor.";

            // Evaluamos el tipo de excepción que lanzamos en nuestros servicios
            switch (exception)
            {
                case ArgumentNullException:
                    statusCode = HttpStatusCode.BadRequest; // Código 400
                    message = exception.Message;
                    break;

                case InvalidOperationException:
                    statusCode = HttpStatusCode.NotFound; // Código 404 o 400 según prefieras
                    message = exception.Message;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            // Creamos un objeto de respuesta anónimo para enviarlo como JSON
            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = message
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}