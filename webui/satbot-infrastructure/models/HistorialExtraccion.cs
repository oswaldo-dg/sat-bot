using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace satbot_infrastructure.models
{
    [Table("HistorialExtraccion")]
    public  class HistorialExtraccion
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(450)]
        /// <summary>
        /// Usuario del sistema
        /// </summary>
        public string UsuarioId { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(20)]
        /// <summary>
        /// RFC asociado al usuario
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// Fecha de la extraccion
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Número de registros procesados
        /// </summary>
        public int RegistrosEncontrados { get; set; }

        /// <summary>
        /// INdica si el proceso terminó sin error
        /// </summary>
        public bool Exito { get; set; }

        /// <summary>
        /// Código de error de la ejecución
        /// </summary>
        public string? CodigoError { get; set; }

    }
}
