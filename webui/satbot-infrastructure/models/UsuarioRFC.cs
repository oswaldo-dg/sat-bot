using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot_infrastructure.models
{

    [Table("UsuarioRFC")]
    /// <summary>
    /// Asocia usuarios con un RFC
    /// </summary>
    public class UsuarioRFC
    {

        public UsuarioRFC()
        {
            Validado = false;
            SecretoId = null;
        }

        [MaxLength(450)]
        /// <summary>
        /// Usuario del sistema
        /// </summary>
        public string UsuarioId { get; set; }

        [MaxLength(20)]
        /// <summary>
        /// RFC asociado al usuario
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// Nombre o razón social del RFC
        /// </summary>
        public string Denominacion { get; set; }

        [DefaultValue(false)]
        /// <summary>
        /// Indica si la propiedad del RFC ha sido validada
        /// </summary>
        public bool Validado { get; set; }

        /// <summary>
        /// Indica si el RFC se encuentra en periodo de evaluación
        /// </summary>
        public bool EnEvaluacion { get; set; }

        /// <summary>
        /// fecha de inicio de la evaluacón
        /// </summary>
        public DateTime? InicioEvaluacion { get; set; }

        /// <summary>
        /// Fecha de finalización de la evaluación
        /// </summary>
        public DateTime? FinEvaluacion { get; set; }

        /// <summary>
        /// Almaena la fecha de validez del certificado
        /// </summary>
        public DateTime? CertificadoValidoHasta { get; set; }


        [DefaultValue(1000)]
        /// <summary>
        /// Especifica el máxicmo número de CFDIS, XMLt PDFS que pueden ser descargados durante la evaluación
        /// </summary>
        public int MaximoCFDIS { get; set; }


        [DefaultValue(null)]
        /// <summary>
        /// Identificador único del secreto asociado a los datos del RFC
        /// </summary>
        public string? SecretoId { get; set; }


        public ApplicationUser Usuario { get; set; }

    }
}
