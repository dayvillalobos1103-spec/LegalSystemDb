# ⚖️ LegalSystem API

Este es un sistema de gestión para bufetes jurídicos desarrollado con **.NET 8** y **PostgreSQL**.

---

## 🚀 Estado del Proyecto
La API se encuentra actualmente conectada a una base de datos en la nube a través de **Render**, lo que permite la persistencia de datos fuera del entorno local.

## 🛠️ Tecnologías Utilizadas
* **Backend:** ASP.NET Core Web API
* **Base de Datos:** PostgreSQL (Alojada en Render)
* **ORM:** Entity Framework Core
* **Seguridad:** JWT (JSON Web Tokens) & ASP.NET Identity
* **Mapeo:** AutoMapper
* **Documentación:** Swagger UI

## ⚙️ Configuración del Entorno
Para ejecutar este proyecto, es necesario configurar un archivo `.env` en la raíz con las siguientes variables de entorno:

| Variable | Descripción |
| :--- | :--- |
| `HOST` | URL del servidor de Render |
| `PORT` | Puerto de conexión (5432) |
| `DATABASE` | Nombre de la base de datos |
| `USER` | Usuario de PostgreSQL |
| `PASSWORD` | Contraseña de acceso |

## 📦 Cómo Correr el Proyecto
1. Clonar el repositorio.
2. Configurar el archivo `.env`.
3. Ejecutar `dotnet ef database update` para sincronizar migraciones.
4. Presionar `F5` o ejecutar `dotnet run`.