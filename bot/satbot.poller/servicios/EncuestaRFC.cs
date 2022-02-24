using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.poller.servicios
{
    public class EncuestaRFC
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Emitidos { get; set; }
        public bool Recibidos { get; set; }

        public bool CanceladosEmitidos { get; set; }

        public bool CanceladosRecibidos { get; set; }

        public bool DescargarXML { get; set; }

        public bool DescargarPDF { get; set; }

        public int IntervaloReceptor { get; set; }
        public int IntervaloEmisor { get; set; }

    }
}
