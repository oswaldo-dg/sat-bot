using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot_infrastructure.models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UsuariosRFC = new HashSet<UsuarioRFC>();
            ConfiguracionesExtraccionRFC = new HashSet<ConfiguracionExtraccionRFC>();
            PluginsRFC = new HashSet<PluginRFC>();
        }

        /// <summary>
        /// Fecha del ultimo acceso del usuario
        /// </summary>
        public DateTime? UltimoAcceso { get; set; }

        /// <summary>
        /// Fecah de regisrto del usuario
        /// </summary>
        public DateTime FechaRegistro { get; set; }


        public virtual ICollection <UsuarioRFC> UsuariosRFC { get; set; }
        public virtual ICollection<ConfiguracionExtraccionRFC> ConfiguracionesExtraccionRFC { get; set; }

        public virtual ICollection<PluginRFC> PluginsRFC { get; set; }

    }
}
