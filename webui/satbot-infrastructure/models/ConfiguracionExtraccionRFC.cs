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
    [Table("ConfiguracionExtraccionRFC")]
    /// <summary>
    /// Almacena la configuración de extracción de CFDIS para un RFC/Usuario
    /// </summary>
    public class ConfiguracionExtraccionRFC
    {
        public ConfiguracionExtraccionRFC()
        {
            ExtraerMetadatosEmisor = false;
            ExtraerMetadatosReceptor = false;
            ExtraerPDFEmisor = false;
            ExtraerPDFReceptor = false;
            ExtraerXMLEmisor = false;
            ExtraerXMLReceptor = false;
            ExtraerCanceladosReceptor=false;
            ExtraerCanceladosReceptor= false;
            Habilitada = false;
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
        /// Fecha de inicio de la extracción
        /// </summary>
        public DateTime FechaInicial { get; set; }

        /// <summary>
        /// Determina si deben extraerse datos de CFID del emisor
        /// </summary>
        public bool ExtraerMetadatosEmisor { get; set; }

        /// <summary>
        /// Determina si deben extraerse datos de CFID del receptor
        /// </summary>
        public bool ExtraerMetadatosReceptor { get; set; }


        /// <summary>
        /// Determina si deben extraerse datos de XML del emisor
        /// </summary>
        public bool ExtraerXMLEmisor { get; set; }

        /// <summary>
        /// Determina si deben extraerse datos de XML del receptor
        /// </summary>
        public bool ExtraerXMLReceptor { get; set; }

        /// <summary>
        /// Determina si deben extraerse datos de PDF del receptor
        /// </summary>
        public bool ExtraerPDFReceptor { get; set; }

        /// <summary>
        /// Determina si deben extraerse datos de PDF del emisor
        /// </summary>
        public bool ExtraerPDFEmisor { get; set; }

        /// <summary>
        /// Determina si deben extraerse datos de PDF del receptor
        /// </summary>
        public bool ExtraerCanceladosReceptor { get; set; }

        /// <summary>
        /// Determina si deben extraerse datos de PDF del emisor
        /// </summary>
        public bool ExtraerCanceladosEmisor { get; set; }


        /// <summary>
        /// Determina si la configuración se encuentra activa
        /// </summary>
        public bool Habilitada { get; set; }

        public ApplicationUser Usuario { get; set; }

    }
}
