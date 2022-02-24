using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace satbot.common
{

    public enum TipoNotificacion
    {
        Ninguno=0, Exito=2, Advertencia=3, Error=4
    }

    public enum EstadoProcesamiento
    {
        Ninguno = 0,Inicio=1, Ejecucion = 2, FinalizadoOk = 3, FinalizadoError = 4
    }

    public class Constantes
    {
    }
}
