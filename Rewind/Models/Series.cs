using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rewind.Models
{
    /// <summary>
    /// Descrição de cada Série
    /// </summary>
    public class Series
    {
        public Series()
        {
            //Inicializa a lista de comentário pertencentes a esta série
            ListaDeComentarios = new HashSet<Comentarios>();
        }
        /// <summary>
        /// Identificação da série
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Titulo da Serie
        /// </summary>
        [Required]
        public string Titulo { get; set; }
        /// <summary>
        /// Sinopse da série
        /// </summary>
        [Required]
        public string Sinopse { get; set; }
        /// <summary>
        /// Número de episódios
        /// </summary>
        [Required]
        public int Episodios { get; set; }
        /// <summary>
        /// Estado da série, se está por acabar ou já acabou
        /// </summary>
        [Required]
        public string Estado { get; set; }
        /// <summary>
        /// Ano em que a série foi publicada
        /// </summary>
        
        public int Ano { get; set; }
        /// <summary>
        /// Foto de capa de série
        /// </summary>
        public string Imagem { get; set; }
        /// <summary>
        /// Data de publicação da serie
        /// </summary>
        public DateTime Data { get; set; }
        /// <summary>
        /// FK do estudio
        /// </summary>
        [ForeignKey(nameof(Estudio))]
        [Display(Name = "Estudio")]
        public int EstudioID { get; set; } // atributo para ser usado no SGBD e no C#. Representa a FK para o estudio
        public Estudios Estudio { get; set; } // atributo para ser usado no C#. Representa a FK para o estudio

        /// <summary>
        /// Lista de comentarios da série
        /// </summary>
        public ICollection<Comentarios> ListaDeComentarios { get; set; }
    }
}
