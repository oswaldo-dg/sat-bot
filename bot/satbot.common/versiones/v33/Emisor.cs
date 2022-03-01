using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace satbot.common.versiones.v33
{
    public class Emisor
    {

        /// <summary>
        /// Atributo requerido para registrar la Clave del Registro Federal de Contribuyentes correspondiente al contribuyente emisor del comprobante.
        /// </summary>
        [Required]
        [MaxLength(13)]
        public string Rfc { get; set; }

        /// <summary>
        /// Atributo opcional para registrar el nombre, denominación o razón social del contribuyente emisor del comprobante.
        /// </summary>
        [MaxLength(254)]
        public string Nombre { get; set; }


        /// <summary>
        /// Atributo requerido para incorporar la clave del régimen del contribuyente emisor al que aplicará el efecto fiscal de este comprobante.
        /// c_RegimenFiscal
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string RegimenFiscal { get; set; }
    }
}
