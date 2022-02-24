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
        private (bool OK, string Error) PaginaInicial()
        {
            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/";
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.Found)
                {
                    RegenerarCookies(mycookies);
                    ok = true;
                }
                else
                {
                    error = $"PaginaInicial Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response.StatusCode == HttpStatusCode.Found)
                {
                    RegenerarCookies(mycookies);
                    ok = true;
                }
                else
                {
                    error = $"PaginaInicial error {ex}";
                }

            }
            catch (Exception ex)
            {
                error = $"PaginaInicial error {ex}";
            }

            return (ok, error);


        }


        private (bool OK, string Error) LoginValido(string wresult)
        {
            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/";
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);
                rq.Referer = "https://cfdiau.sat.gob.mx/";
                rq.Method = "POST";

                valoresPost valores = new valoresPost();


                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(valores.obtienePostString());
                rq.ContentType = "application/x-www-form-urlencoded";
                rq.ContentLength = postDataBytes.Length;

                using (Stream stream = rq.GetRequestStream())
                {
                    stream.Write(postDataBytes, 0, postDataBytes.Length);
                    stream.Close();
                }

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.Redirect)
                {
                    RegenerarCookies(mycookies);
                    ok = true;
                }
                else
                {
                    error = $"LoginValido Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response.StatusCode == HttpStatusCode.Found)
                {
                    RegenerarCookies(mycookies);
                    ok = true;
                }
                else
                {
                    error = $"LoginValido error {ex}";
                }

            }
            catch (Exception ex)
            {
                error = $"LoginValido error {ex}";
            }

            return (ok, error);


        }





        private (bool OK, string WResult, string Error) IntentoLogin(string token, string guid)
        {
            string URL = "https://cfdiau.sat.gob.mx/nidp/app/login?id=SATx509Custom&sid=0&option=credential&sid=0";
            string error = null;
            string WResult = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);
                rq.Referer = "https://cfdiau.sat.gob.mx/nidp/wsfed/ep?id=SATUPCFDiCon&sid=0&option=credential&sid=0";
                rq.Method = "POST";

                valoresPost valores = new valoresPost();
                valores.addPar("token", token);
                valores.addPar("credentialsRequired", "CERT");
                valores.addPar("guid", guid);
                valores.addPar("ks", "null");
                valores.addPar("seeder", "");
                valores.addPar("arc", "");
                valores.addPar("tan", "");
                valores.addPar("placer", "");
                valores.addPar("secuence", "");
                valores.addPar("urlApplet", "https://cfdiau.sat.gob.mx/nidp/app/login?id=SATx509Custom");
                valores.addPar("fert", FechaVencimientoZ());

                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] postDataBytes = encoding.GetBytes(valores.obtienePostString());
                rq.ContentType = "application/x-www-form-urlencoded";
                rq.ContentLength = postDataBytes.Length;


                using (Stream stream = rq.GetRequestStream())
                {
                    stream.Write(postDataBytes, 0, postDataBytes.Length);
                    stream.Close();
                }

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(mycookies);
                    StreamReader sreader = new StreamReader(r.GetResponseStream());
                    CurrentResponseBody = sreader.ReadToEnd();
                    WResult = CurrentResponseBody.ObtienetokeWResultLogin();
                    ok = !string.IsNullOrEmpty(WResult);
                }
                else
                {
                    error = $"IntentoLogin Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                error = $"IntentoLogin error {ex}";
            }

            return (ok, WResult, error);


        }



        private (bool OK, string uuid, string Error) FormaLoginFirma()
        {
            string URL = "https://cfdiau.sat.gob.mx/nidp/app/login?id=SATx509Custom&sid=0&option=credential&sid=0";
            string error = null;
            string uuid = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);
                rq.Referer = "https://cfdiau.sat.gob.mx/nidp/app/login?id=SATx509Custom&sid=0&option=credential&sid=0";

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(mycookies);
                    StreamReader sreader = new StreamReader(r.GetResponseStream());
                    CurrentResponseBody = sreader.ReadToEnd();
                    uuid = CurrentResponseBody.Obtienetokenuuid();
                    ok = !string.IsNullOrEmpty(uuid);
                }
                else
                {
                    error = $"FormaLoginFirma Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                error = $"FormaLoginFirma error {ex}";
            }

            return (ok, uuid, error);


        }



        private (bool OK, string Error) FormaLoginCaptcha(string referer)
        {
            string URL = "https://cfdiau.sat.gob.mx/nidp/wsfed/ep?id=SATUPCFDiCon&sid=0&option=credential&sid=0";
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);
                rq.Referer = referer;

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(mycookies);
                    ok = true;
                }
                else
                {
                    error = $"FormaLoginCaptcha Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                error = $"FormaLoginCaptcha error {ex}";
            }

            return (ok, error);


        }


        private (bool OK, string Error) RedirectInicial(string Location)
        {
            string URL = Location;
            string error = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.OK)
                {
                    RegenerarCookies(mycookies);
                    ok = true;
                }
                else
                {
                    error = $"RedirectInicial Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                error = $"RedirectInicial error {ex}";
            }

            return (ok, error);


        }

     private (bool OK, string Location, string Error) LLamadaInicial()
        {
            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/";
            string error = null;
            string location = null;
            bool ok = false;
            HttpWebResponse r = null;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);

                r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.Redirect)
                {
                    location = r.Headers[HttpResponseHeader.Location];
                    RegenerarCookies(mycookies);
                    ok = true;
                }
                else
                {
                    error = $"LLamadaInicial Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;

                if(response.StatusCode== HttpStatusCode.Found)
                {
                    location = response.Headers[HttpResponseHeader.Location];
                    RegenerarCookies(mycookies);
                    ok = true;

                } else
                {
                    error = $"LLamadaInicial error {ex}";
                }
                
            }catch (Exception ex)
            {
                error = $"LLamadaInicial error {ex}";
            }

            return (ok, location, error);


        }
        private (bool OK, string Location, string Error) Logout()
        {
            string URL = "https://portalcfdi.facturaelectronica.sat.gob.mx/logout.aspx?salir=y";
            string error = null;
            string location = null;
            bool ok = false;
            try
            {

                HttpWebRequest rq = BrowserRequest(URL);

                var r = (HttpWebResponse)rq.GetResponse();
                if (r.StatusCode == HttpStatusCode.Redirect)
                {
                    location = r.Headers[HttpResponseHeader.Location];
                    ok = true;
                }
                else
                {
                    error = $"LLamadaInicial Respuesta incorrecta {r.StatusCode}";
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                if (response.StatusCode == HttpStatusCode.Found)
                {
                    location = response.Headers[HttpResponseHeader.Location];
                    ok = true;
                }
                else
                {
                    error = $"LLamadaInicial error {ex}";
                }

            }
            catch (Exception ex)
            {
                error = $"LLamadaInicial error {ex}";
            }

            return (ok, location, error);


        }
    }
}
