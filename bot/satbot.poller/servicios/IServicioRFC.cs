using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.poller.servicios
{
    public interface IServicioRFC
    {

        Task<EncuestaRFC> ObtieneEncuesta();

    }
}
