using System;

namespace Rewind.Models
{
    /// <summary>
    /// transporta os dados das series
    /// </summary>
    public class SeriesAPIViewModel
    {
        public int IdSerie { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public int Episodios { get; set; }
        public string Estado { get; set; }
        public int Ano { get; set; }
        public string Imagem { get; set; }
        public DateTime Data { get; set; }
        public string NomeEstudio { get; set; }
    }

    public class EstudiosAPIViewModel
    {
        public int ID { get; set; }
        public string Estudios { get; set; }
        public string Pais { get; set; }
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
