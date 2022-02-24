using satbot.common.eventos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace satbot.poller
{
    public partial class Poller
    {
        public event EventHandler<Notificacion> HandlerNotificacion;
        protected virtual void OnNotificacion(Notificacion e)
        {
            HandlerNotificacion?.Invoke(this, e);
        }

        public event EventHandler<Procesamiento> HandlerProcesamiento;
        protected virtual void OnProcesamiento(Procesamiento e)
        {
            HandlerProcesamiento?.Invoke(this, e);
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private string FechaVencimientoZ()
        {
            X509Certificate2 cert = new X509Certificate2(PathPFX, Password);
            return $"{cert.NotAfter.ToUniversalTime().ToString("yyMMddHHmmss")}Z";
        }

        private CookieContainer CopyCookieContainer(CookieContainer container)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, container);
                stream.Seek(0, SeekOrigin.Begin);
                return (CookieContainer)formatter.Deserialize(stream);
            }
        }

        private HttpWebRequest BrowserRequest(string URL, CookieContainer cookies=null)
        {
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(URL);
            rq.Accept = "text/html, application/xhtml+xml, */*";
            rq.Headers.Add("Accept-Encoding: gzip, deflate");
            rq.Headers.Add("Accept-Language: es-MX,es;q=0.5");
            rq.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
            rq.Timeout = HttpBasicTimeout;
            rq.AllowAutoRedirect = false;
            rq.CookieContainer = cookies ?? mycookies;
            rq.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            rq.KeepAlive = true;
            return rq;
        }

        public string FirmaAcceso(string UUID)
        {
            X509Certificate2 cert = new X509Certificate2(this.PathPFX, this.Password, X509KeyStorageFlags.Exportable);
            string serie = Reverse(Encoding.UTF8.GetString(cert.GetSerialNumber()));

            var dataToSign = Encoding.UTF8.GetBytes($"{UUID}|{this.RFC}|{serie}");

            var rsa = (RSA)cert.PrivateKey;
            var xml = RSAHelper.ToXmlString(rsa, true);
            var parameters = RSAHelper.GetParametersFromXmlString(rsa, xml);

            // generate new private key in correct format
            var cspParams = new CspParameters()
            {
                ProviderType = 24,
                ProviderName = "Microsoft Enhanced RSA and AES Cryptographic Provider"
            };
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.ImportParameters(parameters);

            // sign data
            var signedBytes = rsaCryptoServiceProvider.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            byte[] sb = Encoding.UTF8.GetBytes(Convert.ToBase64String(signedBytes));
            byte[] token = Encoding.UTF8.GetBytes($"{Convert.ToBase64String(dataToSign)}#{Convert.ToBase64String(sb)}");
            return Convert.ToBase64String(token);
        }


        /// <summary>
        ///  Esta función ese ecjuta corectamente en .NET 4.5
        /// </summary>
        /// <param name="UUID"></param>
        /// <returns></returns>
        public string FirmaAccesoX(string UUID)
        {
            X509Certificate2 cert = new X509Certificate2(this.PathPFX, this.Password);
            string serie = Reverse(Encoding.UTF8.GetString(cert.GetSerialNumber()));



            var bytes = Encoding.UTF8.GetBytes($"{UUID}|{this.RFC}|{serie}");

            RSACryptoServiceProvider rsacsp = (RSACryptoServiceProvider)cert.PrivateKey;
            CspParameters cspParam = new CspParameters
            {
                KeyContainerName = rsacsp.CspKeyContainerInfo.KeyContainerName,
                KeyNumber = rsacsp.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2
            };
            RSACryptoServiceProvider aescsp = new RSACryptoServiceProvider(cspParam)
            {
                PersistKeyInCsp = false
            };

            byte[] signed = aescsp.SignData(bytes, "sha1");
            byte[] sb = Encoding.UTF8.GetBytes(Convert.ToBase64String(signed));

            byte[] token = Encoding.UTF8.GetBytes($"{Convert.ToBase64String(bytes)}#{Convert.ToBase64String(sb)}");
            return Convert.ToBase64String(token);
        }

        private void RegenerarCookies(CookieContainer container)
        {
            Hashtable table = (Hashtable)this.mycookies.GetType().InvokeMember("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, this.mycookies, new object[0]);
            CookieCollection copia = new CookieCollection();
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField, null, pathList, new object[0]);
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                {
                    foreach (Cookie cookie in colCookies)
                    {
                        copia.Add(cookie);
                        if (cookie.HttpOnly)
                        {
                        }
                    }
                }
            }

            container = new CookieContainer();
            foreach (Cookie cookie in copia)
            {
#if DEBUG
                Console.WriteLine($"{cookie.Name}:{cookie.Domain}:{cookie.Value}");
#endif
                if (cookie.Name == "bbbbbbbbbbbbbbb")
                {
                    cookie.Path = "/nidp/";
                }
                container.Add(cookie);
            }
        }

    }
}
