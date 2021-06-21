using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rewind.Models
{
    public class Comentarios
    {
        /// <summary>
        /// Chave primaria de ligação entre utilizadores e series
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// FK do utilizador
        /// </summary>
        [ForeignKey(nameof(Utilizador))]
        [Display(Name = "Utilizador")]
        public int UtilizadoresID { get; set; }
        public Utilizadores Utilizador { get; set; }
        /// <summary>
        /// FK da Serie
        /// </summary>
        [ForeignKey(nameof(Serie))]
        [Display(Name = "Serie")]
        public int SeriesID { get; set; }
        public Series Serie { get; set; }
        /// <summary>
        /// Estado do comentario
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// Data de publicação do comentario
        /// </summary>
        public DateTime Data { get; set; }
        /// <summary>
        /// Comentario feito à serie por um utilizador
        /// </summary>
        public string Comentario { get; set; }
        /// <summary>
        /// Avaliação feita à serie por um utilizador
        /// </summary>
        [Range(1,5, ErrorMessage = "O valor das estrelas tem que ser entre {1} e {2}.")]
        [Required(ErrorMessage ="Adicione uma classificação")]
        [Display(Name ="Estrelas entre 1 e 5")]
        public int Estrelas { get; set; }
    }
}
