using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.poller.servicios
{
    public class ServicioDemoRFC : IServicioRFC
    {
        public ServicioDemoRFC() { 
        }

        public async Task<EncuestaRFC> ObtieneEncuesta() {
            await Task.Delay(0);
            return new EncuestaRFC() { 
                CanceladosEmitidos = true, 
                CanceladosRecibidos = true, 
                DescargarPDF = true, 
                DescargarXML = true, 
                Emitidos = true, 
                FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0,0,0,0),
                FechaFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59, 999),
                IntervaloEmisor =1440,
                IntervaloReceptor = 1440,
                Recibidos = true };
        }
    }
}
