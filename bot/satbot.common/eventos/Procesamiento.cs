using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.common.eventos
{
    public class Procesamiento: EventArgs
    {
        public Procesamiento(EstadoProcesamiento EstadoProcesamiento, string Error = null)
        {
            this.EstadoProcesamiento = EstadoProcesamiento;
            this.Error = Error;
        }

        public string Error { get; set; }
        public EstadoProcesamiento EstadoProcesamiento { get; set; }

        public override string ToString()
        {
            return ($"{EstadoProcesamiento}\t{Error}");
        }
    }
}
