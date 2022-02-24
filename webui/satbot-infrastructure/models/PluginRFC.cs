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
    [Table("PluginRFC")]
    /// <summary>
    /// Determina cuales son los plugins aplicados a una cuenta de extracción de datos
    /// </summary>
    public class PluginRFC
    {
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


        [MaxLength(250)]
        /// <summary>
        /// Identificador del plugin
        /// </summary>
        public string PluginId { get; set; }


        [DefaultValue(false)]
        /// <summary>
        /// Estado de actvuidad del plugin
        /// </summary>
        public bool Activo { get; set; }


        public ApplicationUser Usuario { get; set; }
    }
}
