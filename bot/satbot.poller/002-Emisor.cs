using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace satbot.poller
{
    public partial class Poller
    {

        private async Task ProcesaEmisor()
        {

        }

        private (bool OK, string Error) PaginaInicialEmisor()
        {
            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaEmisor.aspx";
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                   // RegenerarCookies();
                    ok = true;
                }
                else
                {
                    error = $"PaginaInicial Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                error = $"PaginaInicial error {ex}";
            }

            return (ok, error);


        }
    }
}
