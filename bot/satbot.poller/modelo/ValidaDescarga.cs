using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.poller.modelo
{
    public class ValidaDescarga
    {
        public ContenidoValidaDescarga d { get; set; }
    }

    public class ContenidoValidaDescarga
    {
        public string __type { get; set; }
        public int Codigo { get; set; }
        public int CantidadDisponible { get; set; }
        public int CantidadTope { get; set; }
        public string Mensaje { get; set; }
    }

}
