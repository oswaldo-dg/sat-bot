using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace satbot.common.versiones.v33
{
    /// <summary>
    /// Nodo opcional para precisar la información de los comprobantes relacionados.
    /// </summary>
    public class CfdiRelacionados
    {

        public CfdiRelacionados()
        {
            CfdiRelacionado = new List<CfdiRelacionado>();
        }

        /// <summary>
        /// Nodo requerido para precisar la información de los comprobantes relacionados.
        /// </summary>
        public List<CfdiRelacionado> CfdiRelacionado { get; set; }
        /// <summary>
        /// Atributo requerido para indicar la clave de la relación que existe entre éste que se esta generando y el o los CFDI previos.
        /// c_TipoRelacion
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string TipoRelacion { get; set; }

    }


    public class CfdiRelacionado
    {
        /// <summary>
        /// Atributo requerido para registrar el folio fiscal (UUID) de un CFDI relacionado con el presente comprobante, por ejemplo: Si el CFDI relacionado es un comprobante de traslado que sirve para registrar el movimiento de la mercancía. Si este comprobante se usa como nota de crédito o nota de débito del comprobante relacionado. Si este comprobante es una devolución sobre el comprobante relacionado. Si éste sustituye a una factura cancelada.
        /// </summary>
        [Required]
        [MaxLength(36)]
        [RegularExpression("[a-f0-9A-F]{8}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{12}")]
        public string UUID { get; set; }
    }

}
