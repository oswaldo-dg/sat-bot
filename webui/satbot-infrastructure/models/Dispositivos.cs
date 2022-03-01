using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot_infrastructure.models
{
    public class Dispositivos
    {
        /// <summary>
        /// Identificador único del dispositivo
        /// </summary>
        public Guid Id { get; set; }

        [MaxLength(450)]
        /// <summary>
        /// Usuario del sistema dueño del dispositivo
        /// </summary>
        public string UsuarioId { get; set; }

        /// <summary>
        /// Marca del dispositivo
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Modelo del dispositivo
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Activado { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UltimaSincronizacion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VersionSoftware { get; set; }

        /// <summary>
        /// Dirección de red interna (para su localización en la LAN)
        /// </summary>
        public string DireccionRedIntera { get; set; }

    }
}
