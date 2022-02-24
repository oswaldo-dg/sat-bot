using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.common.eventos
{
    public class Notificacion: EventArgs
    {

        public Notificacion(string Modulo, string Mensaje, TipoNotificacion TipoNotificacion = TipoNotificacion.Ninguno, string Error = null)
        {
            this.Modulo = Modulo;
            this.Mensaje = Mensaje;
            this.Error = Error;
            this.TipoNotificacion = TipoNotificacion;
        }

        public string Modulo { get; set; }
        public string Mensaje { get; set; }
        public string Error { get; set; }
        public TipoNotificacion TipoNotificacion { get; set; }

        public override string ToString()
        {
            return ($"{TipoNotificacion}\t{Modulo}\t{Mensaje}\t{Error}");
        }

    }
}
