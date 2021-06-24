using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rewind.Models
{
    /// <summary>
    /// Descrição dos estudios
    /// </summary>
    public class Estudios
    {
        public Estudios()
        {
            //Inicializa a lista de séries pertencente a este estudio
            ListaDeSeries = new HashSet<Series>();
        }
        /// <summary>
        /// Identificação do estudio
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Nome do estudio
        /// </summary>
        [Required(ErrorMessage = "Por favor dê um nome ao Estudio.")]
        public string Estudio { get; set; }
        /// <summary>
        /// Pais do estudio.
        /// </summary>
        [Required(ErrorMessage = "Por favor escolha o nome do pais.")]
        public string Pais { get; set; }
        /// <summary>
        /// Estudio foi apagado ou não.
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// lista das séries associados ao estudio
        /// </summary>
        public ICollection<Series> ListaDeSeries { get; set; }
    }
}
