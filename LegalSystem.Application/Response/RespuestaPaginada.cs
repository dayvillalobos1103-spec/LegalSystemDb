using System;
using System.Collections.Generic;
using System.Linq;

namespace LegalSystem.Application.Response
{
    public class RespuestaPaginada<T>
    {
        public IEnumerable<T> Elementos { get; set; } = Enumerable.Empty<T>();
        public int TotalPaginas { get; set; }
        public int NumeroPagina { get; set; }
        public int TotalElementos { get; set; }
        public int TamanoPagina { get; set; }

        public RespuestaPaginada(IEnumerable<T> elementos, int totalElementos, int numeroPagina, int tamanoPagina)
        {
            Elementos = elementos;
            TotalElementos = totalElementos;
            NumeroPagina = numeroPagina < 1 ? 1 : numeroPagina;
            TamanoPagina = tamanoPagina < 1 ? 10 : tamanoPagina;

            // Esta línea calcula cuántas páginas hay en total (ej. 25 elementos de 10 en 10 = 3 páginas)
            TotalPaginas = (int)Math.Ceiling(totalElementos / (double)TamanoPagina);
        }
    }
}