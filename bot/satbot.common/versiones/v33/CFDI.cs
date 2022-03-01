using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace satbot.common.versiones.v33
{
    public class CFDI
    {

        public CFDI()
        {
            Version = "3.3";
            CfdiRelacionados = new List<CfdiRelacionados>();
        }


        /// <summary>
        /// Nodo requerido para precisar la información del contribuyente receptor del comprobante.
        /// </summary>
        public Receptor Receptor { get; set; }

        /// <summary>
        /// Nodo requerido para expresar la información del contribuyente emisor del comprobante.
        /// </summary>
        public Emisor Emisor { get; set; }

        /// <summary>
        /// Nodo opcional para precisar la información de los comprobantes relacionados.
        /// </summary>
        public List<CfdiRelacionados> CfdiRelacionados { get; set; }

        /// <summary>
        /// Atributo condicional para registrar la clave de confirmación que entregue el PAC para expedir el comprobante con importes grandes, con un tipo de cambio fuera del rango establecido o con ambos  casos. Es requerido cuando se registra un tipo de cambio o un total fuera del rango establecido.
        /// </summary>
        [MaxLength(5)]
        [RegularExpression("[0-9a-zA-Z]{5}")]
        public string Confirmacion { get; set; }

        /// <summary>
        /// Atributo requerido para incorporar el código postal del lugar de expedición del comprobante (domicilio de la matriz o de la sucursal).
        /// c_CodigoPostal
        /// </summary>
        [Required]
        public string LugarExpedicion { get; set; }

        /// <summary>
        /// Atributo condicional para precisar la clave del método de pago que aplica para este comprobante fiscal digital por Internet, conforme al Artículo 29-A fracción VII incisos a y b del CFF.
        /// c_MetodoPago
        /// </summary>
        [MaxLength(3)]
        public string MetodoPago { get; set; }

        /// <summary>
        /// Atributo requerido para expresar la clave del efecto del comprobante fiscal para el contribuyente emisor.
        /// c_TipoDeComprobante
        /// </summary>
        [Required]
        [MaxLength(1)]
        public string TipoDeComprobante { get; set; }

        /// <summary>
        /// Atributo requerido para representar la suma del subtotal, menos los descuentos aplicables, más las contribuciones recibidas (impuestos trasladados - federales o locales, derechos, productos, aprovechamientos, aportaciones de seguridad social, contribuciones de mejoras) menos los impuestos retenidos. Si el valor es superior al límite que establezca el SAT en la Resolución Miscelánea Fiscal vigente, el emisor debe obtener del PAC que vaya a timbrar el CFDI, de manera no automática, una clave de confirmación para ratificar que el valor es correcto e integrar dicha clave en el atributo Confirmacion. No se permiten valores negativos.
        /// </summary>
        [Required]
        public decimal Total { get; set; }


        /// <summary>
        /// Atributo condicional para representar el tipo de cambio conforme con la moneda usada. Es requerido cuando la clave de moneda es distinta de MXN y de XXX. El valor debe reflejar el número de pesos mexicanos que equivalen a una unidad de la divisa señalada en el atributo moneda. Si el valor está fuera del porcentaje aplicable a la moneda tomado del catálogo c_Moneda, el emisor debe obtener del PAC que vaya a timbrar el CFDI, de manera no automática, una clave de confirmación para ratificar que el valor es correcto e integrar dicha clave en el atributo Confirmacion.
        /// </summary>
        public decimal TipoCambio { get; set; }

        /// <summary>
        /// Atributo requerido para identificar la clave de la moneda utilizada para expresar los montos, cuando se usa moneda nacional se registra MXN. Conforme con la especificación ISO 4217.
        /// catCFDI:c_Moneda
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string Moneda { get; set; }

        /// <summary>
        /// Atributo condicional para representar el importe total de los descuentos aplicables antes de impuestos. No se permiten valores negativos. Se debe registrar cuando existan conceptos con descuento.
        /// </summary>
        public decimal Descuento { get; set; }

        /// <summary>
        /// Atributo requerido para representar la suma de los importes de los conceptos antes de descuentos e impuesto. No se permiten valores negativos.
        /// </summary>
        [Required]
        public decimal SubTotal { get; set; }


        /// <summary>
        /// Atributo condicional para expresar las condiciones comerciales aplicables para el pago del comprobante fiscal digital por Internet. Este atributo puede ser condicionado mediante atributos o complementos.
        /// </summary>
        [MaxLength(1000)]
        [MinLength(1)]
        [RegularExpression("[^|]{1,1000}")]
        public string CondicionesDePago { get; set; }


        /// <summary>
        /// Atributo requerido que sirve para incorporar el certificado de sello digital que ampara al comprobante, como texto en formato base 64.
        /// </summary>
        [Required]
        public string Certificado { get; set; }

        /// <summary>
        /// Atributo requerido para expresar el número de serie del certificado de sello digital que ampara al comprobante, de acuerdo con el acuse correspondiente a 20 posiciones otorgado por el sistema del SAT.
        /// </summary>
        [Required]
        [MaxLength(20)]
        [RegularExpression("[0-9]{20}")]
        public string NoCertificado { get; set; }

        /// <summary>
        /// Atributo condicional para expresar la clave de la forma de pago de los bienes o servicios amparados por el comprobante. Si no se conoce la forma de pago este atributo se debe omitir.
        /// catCFDI:c_FormaPago
        /// </summary>
        public string FormaPago { get; set; }

        /// <summary>
        /// Atributo requerido para contener el sello digital del comprobante fiscal, al que hacen referencia las reglas de resolución miscelánea vigente. El sello debe ser expresado como una cadena de texto en formato Base 64.
        /// </summary>
        [Required]
        public string Sello { get; set; }

        /// <summary>
        /// Atributo requerido para la expresión de la fecha y hora de expedición del Comprobante Fiscal Digital por Internet. Se expresa en la forma AAAA-MM-DDThh:mm:ss y debe corresponder con la hora local donde se expide el comprobante.
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Atributo opcional para control interno del contribuyente que expresa el folio del comprobante, acepta una cadena de caracteres.
        /// </summary>
        [MaxLength(40)]
        public string Folio { get; set; }

        /// <summary>
        /// Atributo opcional para precisar la serie para control interno del contribuyente. Este atributo acepta una cadena de caracteres.
        /// </summary>
        [MaxLength(25)]
        public string Serie { get; set; }

        /// <summary>
        /// Atributo requerido con valor prefijado a 3.3 que indica la versión del estándar bajo el que se encuentra expresado el comprobante.
        /// </summary>
        [Required]
        public string Version { get; set; }

        
    }
}
