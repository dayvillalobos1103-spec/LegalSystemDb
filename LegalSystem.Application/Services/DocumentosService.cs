using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LegalSystem.Application.DTOs;
using LegalSystem.Application.Interfaces.Repositorio;
using LegalSystem.Application.Interfaces.Services;
using LegalSystem.Domain.Entities;

namespace LegalSystem.Application.Services
{
    public class DocumentoService : IDocumentoService
    {
        private readonly IDocumentoRepository _documentoRepository;
        private readonly Cloudinary _cloudinary;

        public DocumentoService(IDocumentoRepository documentoRepository)
        {
            _documentoRepository = documentoRepository;

            // 1. Leer las credenciales del .env para Cloudinary
            var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
            var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

            Account account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<DocumentoDtos> CrearDocumentoAsync(CrearDocumentoDto dto, string usuarioId)
        {
            if (dto.Archivo == null || dto.Archivo.Length == 0)
                throw new Exception("El archivo está vacío.");

            // 2. Preparar archivo y subirlo a Cloudinary
            using var stream = dto.Archivo.OpenReadStream();

            // Usamos RawUploadParams para que acepte PDFs, Word, etc. (No solo imágenes)
            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(dto.Archivo.FileName, stream),
                Folder = "LegalSystem_Documentos"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Error de Cloudinary: {uploadResult.Error.Message}");

            // 3. Crear entidad y guardar en Base de Datos
            var nuevoDocumento = new Documento
            {
                Nombre = dto.Archivo.FileName,
                RutaUrl = uploadResult.SecureUrl.ToString(), // URL lista para consumir
                FechaSubida = DateTime.UtcNow,
                ClienteId = dto.ClienteId,
                CasoId = dto.CasoId,
                UsuarioId = usuarioId
            };

            var documentoGuardado = await _documentoRepository.AddAsync(nuevoDocumento);

            // 4. Retornar el DTO
            return new DocumentoDtos
            {
                Id = documentoGuardado.Id,
                Nombre = documentoGuardado.Nombre,
                RutaUrl = documentoGuardado.RutaUrl,
                FechaSubida = documentoGuardado.FechaSubida,
                ClienteId = documentoGuardado.ClienteId,
                CasoId = documentoGuardado.CasoId
            };
        }

        public async Task<IEnumerable<DocumentoDtos>> GetAllByUsuarioIdAsync(string usuarioId)
        {
            var documentos = await _documentoRepository.GetAllByUsuarioIdAsync(usuarioId);

            return documentos.Select(d => new DocumentoDtos
            {
                Id = d.Id,
                Nombre = d.Nombre,
                RutaUrl = d.RutaUrl,
                FechaSubida = d.FechaSubida,
                ClienteId = d.ClienteId,
                CasoId = d.CasoId
            });
        }

        public async Task<bool> DeleteAsync(int id, string usuarioId)
        {
            var documento = await _documentoRepository.GetByIdAsync(id);

            // Seguridad: Solo el dueño del documento puede borrarlo
            if (documento == null || documento.UsuarioId != usuarioId)
                return false;

            return await _documentoRepository.DeleteAsync(documento);
        }
    }
}