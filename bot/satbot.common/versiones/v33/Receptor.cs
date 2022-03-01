using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace satbot.common.versiones.v33
{
    public class Receptor
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
        /// Atributo condicional para expresar el número de registro de identidad fiscal del receptor cuando sea residente en el  extranjero. Es requerido cuando se incluya el complemento de comercio exterior.
        /// </summary>
        [MaxLength(40)]
        [MinLength(1)]
        public string NumRegIdTrib { get; set; }


        /// <summary>
        /// Atributo requerido para expresar la clave del uso que dará a esta factura el receptor del CFDI.
        /// c_UsoCFDI
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string UsoCFDI { get; set; }


        /// <summary>
        /// Atributo condicional para registrar la clave del país de residencia para efectos fiscales del receptor del comprobante, cuando se trate de un extranjero, y que es conforme con la especificación ISO 3166-1 alpha-3. Es requerido cuando se incluya el complemento de comercio exterior o se registre el atributo NumRegIdTrib.
        /// c_Pais
        /// </summary>
        public string ResidenciaFiscal { get; set; }
    }
}
