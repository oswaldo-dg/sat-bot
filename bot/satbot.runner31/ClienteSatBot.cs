using satbot.poller;
using satbot.poller.servicios;
using satbot.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace satbot.runner31
{
    public class ClienteSatBot
    {
        private readonly string logPath;
        private readonly string PfxPath;
        private readonly string Paswword;
        private readonly string RFC;
        public ClienteSatBot(string logPath, string PfxPath, string Password, string RFC)
        {
            this.logPath= logPath;
            this.PfxPath= PfxPath;
            this.Paswword= Password;
            this.RFC= RFC;
        }

        public async Task Procesar()
        {
            Console.WriteLine("Starting");
            Poller p = new Poller(new ServicioDemoRFC());
            p.HandlerNotificacion += P_HandlerNotificacion1;
            p.HandlerProcesamiento += P_HandlerProcesamiento1;
            await p.Iniciar(RFC, PfxPath, Paswword);
        }

        private void P_HandlerProcesamiento1(object sender, satbot.common.eventos.Procesamiento e)
        {
            Console.WriteLine(e.ToString());
            Log(e.ToString());
        }

        private void P_HandlerNotificacion1(object sender, satbot.common.eventos.Notificacion e)
        {
            Console.WriteLine(e.ToString());
            Log(e.ToString());
        }


        private void Log(string log)
        {
            try
            {
                File.AppendAllText(logPath, $"{log }\r\n");
            }
            catch (Exception)
            {

            }
        }

    }
}
