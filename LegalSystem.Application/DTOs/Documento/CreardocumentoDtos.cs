using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LegalSystem.Application.DTOs
{
    public class CrearDocumentoDto
    {
        [Required(ErrorMessage = "El archivo es obligatorio.")]
        public IFormFile Archivo { get; set; }

        [Required(ErrorMessage = "El documento debe pertenecer a un cliente.")]
        public int ClienteId { get; set; }

        public int? CasoId { get; set; }
}  }