using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot_infrastructure.models
{
    public class DispositivoRFC
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

        /// <summary>
        /// Identificador único del dispositivo
        /// </summary>
        public Guid DispositivoId { get; set; }

    }
}
