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
        private  int Ano;
        private  int Mes;
        private  int Dia;

       

        public Poller(IServicioRFC servicioRFC)
        {
            // Dos minutos
            HttpBasicTimeout = 2 *60 * 10000;
            this.servicioRFC = servicioRFC;
        }


        public async Task Iniciar(string RFC, string PathPFX, string Password, int Ano, int Mes, int Dia)
        {
            Console.WriteLine("Iniciando");
            this.RFC = RFC.ToUpper();
            this.PathPFX = PathPFX;
            this.Password = Password;
            this.Ano = Ano;
            this.Mes = Mes;
            this.Dia = Dia;

            mycookies = new CookieContainer();
            OnProcesamiento( EstadoProcesamiento.Inicio.ArgProcesamiento());
            OnNotificacion($"Iniciando proceso para {RFC}".ArgNotificacion("Iniciar"));

            Console.WriteLine("---------------");
            var (OK, Location, Error) = LLamadaInicial();
            if (OK)
            {
                Console.WriteLine($"--------------- {Location}");
                if (!string.IsNullOrEmpty(Location))
                {
                    var (OKri, Errorri) = RedirectInicial(Location);
                    if (OKri)
                    {
                        Console.WriteLine($"--------------- Captcha");
                        Task.Delay(500).Wait();
                        var (OKCaptcha, ErrorCaptcha) = FormaLoginCaptcha(Location);
                        if (OKCaptcha)
                        {
                            Console.WriteLine($"--------------- LoginFirma");
                            Task.Delay(250).Wait();
                            var (OKFirma, UUIDFirma,  ErrorFirma) = FormaLoginFirma();
                            if (OKFirma)
                            {

                                Console.WriteLine($"--------------- Firma");
                                string token = FirmaAcceso(UUIDFirma);
                                Console.WriteLine($"--------------- Login");
                                var (OKIntentoLogin, WResult, ErrorIntentoLogin) = IntentoLogin(token, UUIDFirma);
                                if (OKIntentoLogin)
                                {
                                    Console.WriteLine($"--------------- Login valido");
                                    var(OKLogin, ErrLogin) = LoginValido(WResult);
                                    Task.Delay(500).Wait();
                                    if (OKLogin)
                                    {
                                        Console.WriteLine($"--------------- PAgina inicial");
                                        var (OKPinicial, ErrPinicial) = PaginaInicial();
                                        if (OKPinicial)
                                        {
                                            Console.WriteLine($"--------------- Encuesta");
                                            encuesta = await servicioRFC.ObtieneEncuesta();
                                            encuesta.FechaInicio = new DateTime(Ano, Mes, Dia == 0 ? 1 : Dia);
                                            encuesta.FechaFinal = new DateTime(Ano, Mes, DateTime.DaysInMonth(Ano, Mes));

                                            Task.Delay(500).Wait();
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

            await ProcesaReceptor();

            //List<Task> tareas = new List<Task>();
            //await Task.Delay(0);
            //if (encuesta.Recibidos)
            //{
            //    tareas.Add(ProcesaReceptor());
            //}

            //if (encuesta.Emitidos)
            //{
            //    tareas.Add(ProcesaEmisor());
            //}

            //Task.WaitAll(tareas.ToArray());

        }


    }
}
