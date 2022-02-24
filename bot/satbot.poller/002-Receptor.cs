using satbot.common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        private StringDictionary EstadoReceptor = null;
        private CookieContainer cookiesReceptor;

        private (bool OK, string Error) PaginaInicialReceptor()
        {
            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL, cookiesReceptor);

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(cookiesReceptor);
                    StreamReader sreader = new StreamReader(r.GetResponseStream());
                    string body = sreader.ReadToEnd();
                    EstadoReceptor = body.ObtieneEstadoReceptor();
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


        private (bool Ok, string Error) CambiaAReceptorPorFecha()
        {

            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL, cookiesReceptor);
                rq.Referer = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
                rq.Method = "POST";
                rq.Headers.Add("X-MicrosoftAjax", "Delta=true");
                valoresPost valores = new valoresPost();
                valores.pares.AddRange(EstadoReceptor.SeleccionBusquedaReceptorfecha());


                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(valores.obtienePostString());
                rq.ContentType = "application/x-www-form-urlencoded";
                rq.ContentLength = postDataBytes.Length;


                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(cookiesReceptor);
                    StreamReader sreader = new StreamReader(r.GetResponseStream());
                    string body = sreader.ReadToEnd();
                    EstadoReceptor = body.ObtieneEstadoReceptor();
                    ok = true;
                }
                else
                {
                    error = $"Página Inicial Receptor por Fecha respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                error = $"Página Inicial Receptor por Fecha {ex}";
            }

            return (ok, error);

        }


        private void ObtieneUsuarioReceptor()
        {
            OnNotificacion($"Consulta de Usuario Receptor".ArgNotificacion("ObtieneUsuarioReceptor"));
            string URL = $"https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx/ObtieneUsuario";
            try
            {

                HttpWebRequest rq = BrowserRequest(URL, cookiesReceptor);
                rq.Referer = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(cookiesReceptor);
                }

            }
            catch (Exception ex)
            {
                OnNotificacion($"Error Consulta de Usuario Receptor: {ex}".ArgNotificacion("ObtieneUsuarioReceptor", TipoNotificacion.Error));
            }
        }


        private async Task ProcesaReceptor()
        {
            cookiesReceptor = CopyCookieContainer(mycookies);
            var (Okinicial, ErrInicia) = PaginaInicialReceptor();
            if (Okinicial)
            {
                ObtieneUsuarioReceptor();
                var (OkCambioRFecha, ErrorCambioRFecha) = CambiaAReceptorPorFecha();
                if(OkCambioRFecha)
                {
                    DateTime actual = encuesta.FechaInicio;
                    DateTime limiteConsulta = actual.AddMinutes(encuesta.IntervaloReceptor);
                    while (actual <= encuesta.FechaFinal)
                    {

                        OnNotificacion($"Procesando Receptor {actual.ToString("dd/MM/yyyy HH:mm:ss")} - {limiteConsulta.ToString("dd/MM/yyyy HH:mm:ss")}".ArgNotificacion("ProcesaReceptor"));
                        var (OkRXFecha, ErrorRXFecha) = ReceptorPorFecha(actual, limiteConsulta);
                        if(OkRXFecha)
                        {
                            actual = limiteConsulta;
                            limiteConsulta = actual.AddMinutes(encuesta.IntervaloReceptor);
                            ObtieneUsuarioReceptor();
                        } else
                        {
                            break;
                        }
                    }
                } else
                {
                    OnNotificacion($"No fue posible seleccionar la búsqueda por fecha en la página del receptor {ErrorCambioRFecha}".ArgNotificacion("ProcesaReceptor", TipoNotificacion.Error));
                }
            } else
            {
                OnNotificacion($"No fue posible ingresar a la página del receptorr {ErrInicia}".ArgNotificacion("ProcesaReceptor", TipoNotificacion.Error));
            }

            await Task.Delay(0);
        }

   

        private bool MantieneSesionRecibidos()
        {
            string URL = $"https://portalcfdi.facturaelectronica.sat.gob.mx/verificasesion.aspx?_={DateTime.Now.Ticks}";
            try
            {

                HttpWebRequest rq = BrowserRequest(URL, cookiesReceptor);
                rq.Referer = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
                var r = (HttpWebResponse)rq.GetResponse();
                if(r.StatusCode== HttpStatusCode.OK)
                {
                    return true;
                }

            }
            catch (Exception)
            {
            }
            return false;
        }

        private bool ValidaDescargaRecibidos()
        {
            string URL = $"https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx/ValidarDescarga";
            try
            {

                HttpWebRequest rq = BrowserRequest(URL, cookiesReceptor);
                rq.Referer = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
                rq.Method = "POST";
                rq.ContentType = "application/json; charset=UTF-8";

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

            }
            catch (Exception)
            {
            }
            return false;
        }


        private  (bool Ok, string Error) ReceptorPorFecha(DateTime inicio, DateTime  fin)
        {

            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL, cookiesReceptor);
                rq.Referer = "https://portalcfdi.facturaelectronica.sat.gob.mx/ConsultaReceptor.aspx";
                rq.Method = "POST";
                rq.Headers.Add("X-MicrosoftAjax", "Delta=true");
                valoresPost valores = new valoresPost();
                valores.pares.AddRange(EstadoReceptor.BusquedaReceptorfecha(inicio, fin, false));

                Extensiones.ElementosEstadoReceptor.ForEach(e =>
                {
                    valores.addPar(e, EstadoReceptor[e]);
                });
                
                

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(valores.obtienePostString());
                rq.ContentType = "application/x-www-form-urlencoded";
                rq.ContentLength = postDataBytes.Length;


                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(cookiesReceptor);
                    StreamReader sreader = new StreamReader(r.GetResponseStream());
                    string body = sreader.ReadToEnd();
                    EstadoReceptor = body.ObtieneEstadoReceptor();

                    var cfdis = body.ObtieneElementosRecibidos();
                    foreach(var cfdi in cfdis)
                    {
                        if (cfdi.Vigente)
                        {
                            if (MantieneSesionRecibidos())
                            {

                            }   
                        }
                    }

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
