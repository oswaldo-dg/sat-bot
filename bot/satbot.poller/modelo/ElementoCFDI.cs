using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.poller.modelo
{
    public class ElementoCFDI
    {



        /// <summary>
        /// Determina si el elemento corresponde a los emitidos o recibidos
        /// </summary>
        public bool Emitido { get; set; }

        /// <summary>
        /// RFC del emisor, vacío para las emitidas pues es el del RFC en procesamiento
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// Nombre del emisor, vacío para las emitidas pues es el del RFC en procesamiento
        /// </summary>
        public string Nombre { get; set; }


        public string FolioFiscal { get; set; }

        public string FechaEmision { get; set; }

        public string FechaCertificacion { get; set; }

        public string PAC { get; set; }

        public string Total { get; set; }

        public string Efecto { get; set; }

        public string EstatusCancelacion { get; set; }

        public string EstadoComprobante { get; set; }

        public string EstatusProcesoCancelacion { get; set; }

        public string FechaProcesoCancelacion { get; set; }

        public string LinkXML { get; set; }

        public string LinkPDF { get; set; }
        public bool Vigente { get; set; }

    }

}


