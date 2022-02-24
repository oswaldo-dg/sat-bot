using satbot.common;
using satbot.poller.servicios;
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
        private string CurrentResponseBody;
        private string RFC;
        private string PathPFX;
        private string Password;
        private CookieContainer mycookies;
        public int HttpBasicTimeout { get; set; }
        private readonly IServicioRFC servicioRFC;
        private EncuestaRFC encuesta;
        public Poller(IServicioRFC servicioRFC)
        {
            // Dos minutos
            HttpBasicTimeout = 2 *60 * 10000;
            this.servicioRFC = servicioRFC;
        }


        public async Task Iniciar(string RFC, string PathPFX, string Password)
        {
            
            this.RFC = RFC.ToUpper();
            this.PathPFX = PathPFX;
            this.Password = Password;

            mycookies = new CookieContainer();
            OnProcesamiento( EstadoProcesamiento.Inicio.ArgProcesamiento());
            OnNotificacion($"Iniciando proceso para {RFC}".ArgNotificacion("Iniciar"));

            var (OK, Location, Error) = LLamadaInicial();
            if (OK)
            {
                if (!string.IsNullOrEmpty(Location))
                {
                    var (OKri, Errorri) = RedirectInicial(Location);
                    if (OKri)
                    {
                        var (OKCaptcha, ErrorCaptcha) = FormaLoginCaptcha(Location);
                        if (OKCaptcha)
                        {
                            var (OKFirma, UUIDFirma,  ErrorFirma) = FormaLoginFirma();
                            if (OKFirma)
                            {

                                string token = FirmaAcceso(UUIDFirma);
                                var (OKIntentoLogin, WResult, ErrorIntentoLogin) = IntentoLogin(token, UUIDFirma);
                                if (OKIntentoLogin)
                                {
                                    var(OKLogin, ErrLogin) = LoginValido(WResult);
                                    if (OKLogin)
                                    {
                                        var (OKPinicial, ErrPinicial) = PaginaInicial();
                                        if (OKPinicial)
                                        {
                                            encuesta = await servicioRFC.ObtieneEncuesta();
                                            if (encuesta != null)
                                            {
                                                await ProcesaEncuenta();
                                                var (OkLogout, LocationLoout, ErrLogout )  = Logout();

                                            } else
                                            {
                                                OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento("No hay datos de encuesta"));
                                            }

                                        } else
                                        {
                                            OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento(ErrPinicial));
                                        }

                                    } else
                                    {
                                        OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento(ErrLogin));
                                    }
                                    

                                } else
                                {
                                    OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento(ErrorIntentoLogin));
                                }
                            }
                            else
                            {
                                OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento(ErrorFirma));
                            }
                        } else
                        {
                            OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento(ErrorCaptcha));
                        }

                    } else
                    {
                        OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento(Errorri));
                    }
                }
            } else
            {
                OnProcesamiento(EstadoProcesamiento.FinalizadoError.ArgProcesamiento(Error));
            }
            
        }


        private async Task ProcesaEncuenta()
        {
            List<Task> tareas = new List<Task>();
            await Task.Delay(0);
            if (encuesta.Recibidos)
            {
                tareas.Add(ProcesaReceptor());
            }

            if (encuesta.Emitidos)
            {
                tareas.Add(ProcesaEmisor());
            }

            Task.WaitAll(tareas.ToArray());

        }


    }
}
