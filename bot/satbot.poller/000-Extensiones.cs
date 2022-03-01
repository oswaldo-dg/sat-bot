using HtmlAgilityPack;
using satbot.common;
using satbot.common.eventos;
using satbot.poller.modelo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace satbot.poller
{
    public static class Extensiones
    {
        public static List<string> ElementosEstadoReceptor => new List<string>()
                    {
                        "__CSRFTOKEN",
                        "__EVENTTARGET",
                        "__EVENTARGUMENT",
                        "__LASTFOCUS",
                        "__VIEWSTATE",
                        "__VIEWSTATEGENERATOR",
                        "__VIEWSTATEENCRYPTED"
                    };

        public static List<parPost> SeleccionBusquedaReceptorfecha(this StringDictionary estadoReceptor)
        {
            List<parPost> l = new List<parPost>();
            l.Add(new parPost() { clave = "__CSRFTOKEN", valor = estadoReceptor["__CSRFTOKEN"] });
            l.Add(new parPost() { clave = "__EVENTTARGET", valor = "ctl00$MainContent$RdoFechas" });
            l.Add(new parPost() { clave = "__EVENTARGUMENT", valor = "" });
            l.Add(new parPost() { clave = "__LASTFOCUS", valor = "" });
            l.Add(new parPost() { clave = "__VIEWSTATE", valor = estadoReceptor["__VIEWSTATE"] });
            l.Add(new parPost() { clave = "__VIEWSTATEGENERATOR", valor = estadoReceptor["__VIEWSTATEGENERATOR"] });
            l.Add(new parPost() { clave = "__VIEWSTATEENCRYPTED", valor = "" });
            l.Add(new parPost() { clave = "__ASYNCPOST", valor = "true" });

            l.Add(new parPost() { clave = "ctl00$ScriptManager1", valor = "ctl00$MainContent$UpnlBusqueda|ctl00$MainContent$RdoFechas" });
            l.Add(new parPost() { clave = "ctl00$MainContent$TxtUUID", valor = "" });
            l.Add(new parPost() { clave = "ctl00$MainContent$TxtRfcReceptor", valor = "" });
            l.Add(new parPost() { clave = "ctl00$MainContent$hfParametrosMetadata", valor = "" });
            l.Add(new parPost() { clave = "ctl00$MainContent$hfInicialBool", valor = "true" });
            l.Add(new parPost() { clave = "ctl00$MainContent$hfDescarga", valor = "" });
            l.Add(new parPost() { clave = "ctl00$MainContent$FiltroCentral", valor = "RdoFechas" });
            l.Add(new parPost() { clave = "ctl00$MainContent$ddlVigente", valor = "0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$DdlEstadoComprobante", valor = "-1"});
            l.Add(new parPost() { clave = "ctl00$MainContent$ddlComplementos", valor = "-1" });
            l.Add(new parPost() { clave = "ctl00$MainContent$ddlCancelado", valor = "0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlSegundoFin", valor = $"59" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlSegundo", valor = $"0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlMinutoFin", valor = $"59" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlMinuto", valor = $"0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlMes", valor = $"1" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlHoraFin", valor = $"23" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlHora", valor = $"0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlDia", valor = $"0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlAnio", valor = $"{DateTime.Now.Year}" });

            return l;
        }

        public static List<parPost> BusquedaReceptorfecha(this StringDictionary estadoReceptor, DateTime inicio, DateTime fin, bool Cancelado)
        {
            List<parPost> l = new List<parPost>();
            l.Add(new parPost() { clave = "__CSRFTOKEN", valor = estadoReceptor["__CSRFTOKEN"] });
            l.Add(new parPost() { clave = "__EVENTTARGET", valor = "ctl00$MainContent$RdoFechas" });
            l.Add(new parPost() { clave = "__EVENTARGUMENT", valor = "" });
            l.Add(new parPost() { clave = "__LASTFOCUS", valor = "" });
            l.Add(new parPost() { clave = "__VIEWSTATE", valor = estadoReceptor["__VIEWSTATE"] });
            l.Add(new parPost() { clave = "__VIEWSTATEGENERATOR", valor = estadoReceptor["__VIEWSTATEGENERATOR"] });
            l.Add(new parPost() { clave = "__VIEWSTATEENCRYPTED", valor = "" });

            l.Add(new parPost() { clave = "ctl00$ScriptManager1", valor = "ctl00$MainContent$UpnlBusqueda|ctl00$MainContent$RdoFechas" });
            l.Add(new parPost() { clave = "ctl00$MainContent$TxtUUID", valor = "" });
            l.Add(new parPost() { clave = "ctl00$MainContent$FiltroCentral", valor = "RdoFechas" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlAnio", valor = $"{inicio.Year}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlMes", valor = $"{inicio.Month}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlDia", valor = $"{inicio.Day}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlHora", valor = $"{inicio.Hour}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlMinuto", valor = $"{inicio.Minute}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlSegundo", valor = $"{inicio.Second}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlHoraFin", valor = $"{fin.Hour}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlMinutoFin", valor = $"{fin.Minute}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$CldFecha$DdlSegundoFin", valor = $"{fin.Second}" });
            l.Add(new parPost() { clave = "ctl00$MainContent$TxtRfcReceptor", valor = "" });
            l.Add(new parPost() { clave = "ctl00$MainContent$DdlEstadoComprobante", valor = Cancelado ? "0" : "1" });
            l.Add(new parPost() { clave = "ctl00$MainContent$hfInicialBool", valor = "true" });
            l.Add(new parPost() { clave = "ctl00$MainContent$hfDescarga", valor = "" });
            l.Add(new parPost() { clave = "ctl00$MainContent$ddlComplementos", valor = "-1" });
            l.Add(new parPost() { clave = "ctl00$MainContent$ddlVigente", valor = "0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$ddlCancelado", valor = "0" });
            l.Add(new parPost() { clave = "ctl00$MainContent$hfParametrosMetadata", valor = "true" });
            l.Add(new parPost() { clave = "__ASYNCPOST", valor = "true" });
            return l;
        }


        public static List<ElementoCFDI> ObtieneElementosRecibidos(this string html)
        {
            List<ElementoCFDI> l = new List<ElementoCFDI>();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var table = document.DocumentNode.SelectSingleNode($"//table[@id='ctl00_MainContent_tblResult']");
            if (table != null)
            {
                var renglones = table.SelectNodes("//tr");
                if (renglones != null)
                {
                    if (renglones.ToList().Count > 0)
                    {

                        foreach (var renglon in renglones.ToList())
                        {
                            if (renglon.InnerHtml.IndexOf("ListaFolios") > 0)
                            {
                                var celdas = renglon.SelectNodes(renglon.XPath + "/td");
                                if (celdas != null && celdas.Count > 12)
                                {
                                    List<HtmlNode> listaCeldas = celdas.ToList();
                                    var celdaDatos = celdas.ToList()[0];

                                    ElementoCFDI el = new ElementoCFDI();
                                    el.Emitido = false;
                                    el.FolioFiscal = listaCeldas[1].ChildNodes[0].InnerText;
                                    el.RFC = listaCeldas[2].ChildNodes[0].InnerText;
                                    el.Nombre = listaCeldas[3].ChildNodes[0].InnerText;
                                    el.FechaEmision = listaCeldas[6].ChildNodes[0].InnerText;
                                    el.FechaCertificacion = listaCeldas[7].ChildNodes[0].InnerText;
                                    el.PAC = listaCeldas[8].ChildNodes[0].InnerText;
                                    el.Total = listaCeldas[9].ChildNodes[0].InnerText;
                                    el.Efecto = listaCeldas[10].ChildNodes[0].InnerText;
                                    el.EstatusCancelacion = listaCeldas[11].ChildNodes[0].InnerText;
                                    el.EstadoComprobante = listaCeldas[12].ChildNodes[0].InnerText;
                                    el.EstatusProcesoCancelacion = listaCeldas[13].ChildNodes[0].InnerText;
                                    el.FechaProcesoCancelacion = listaCeldas[14].ChildNodes[0].InnerText;
                                    el.FolioFiscal = celdaDatos.SelectSingleNode("//input[@id='ListaFolios']")?.GetAttributeValue("value", "");
                                    el.Vigente = el.EstadoComprobante.Equals("Vigente", StringComparison.InvariantCultureIgnoreCase);

                                    if (el.Vigente)
                                    {
                                        el.LinkXML = celdaDatos.SelectSingleNode("//span[@id='BtnDescarga']")?.GetAttributeValue("onclick", "");
                                        el.LinkPDF = celdaDatos.SelectSingleNode("//span[@id='BtnRI']")?.GetAttributeValue("onclick", "");
                                    }
                                    l.Add(el);
                                }
                            }
                        }

                    };

                }



            }

            return l;
        }

        public static List<ElementoCFDI> ObtieneElementosEmitidos(this string html)
        {
            List<ElementoCFDI> l = new List<ElementoCFDI>();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var table = document.DocumentNode.SelectSingleNode($"//table[@id='ctl00_MainContent_tblResult']");
            if (table != null)
            {
                var renglones = table.SelectNodes("//tr");
                if (renglones != null)
                {
                    if (renglones.ToList().Count > 0)
                    {

                        foreach (var renglon in renglones.ToList())
                        {
                            if (renglon.InnerHtml.IndexOf("ListaFolios") > 0)
                            {
                                var celdas = renglon.SelectNodes(renglon.XPath + "/td");
                                if (celdas != null && celdas.Count > 12)
                                {
                                    List<HtmlNode> listaCeldas = celdas.ToList();
                                    var celdaDatos = celdas.ToList()[0];

                                    ElementoCFDI el = new ElementoCFDI();
                                    el.Emitido = false;
                                    el.FolioFiscal = listaCeldas[1].ChildNodes[0].InnerText;
                                    el.RFC = listaCeldas[4].ChildNodes[0].InnerText;
                                    el.Nombre = listaCeldas[5].ChildNodes[0].InnerText;
                                    el.FechaEmision = listaCeldas[6].ChildNodes[0].InnerText;
                                    el.FechaCertificacion = listaCeldas[7].ChildNodes[0].InnerText;
                                    el.PAC = listaCeldas[8].ChildNodes[0].InnerText;
                                    el.Total = listaCeldas[9].ChildNodes[0].InnerText;
                                    el.Efecto = listaCeldas[10].ChildNodes[0].InnerText;
                                    el.EstatusCancelacion = listaCeldas[11].ChildNodes[0].InnerText;
                                    el.EstadoComprobante = listaCeldas[12].ChildNodes[0].InnerText;
                                    el.EstatusProcesoCancelacion = listaCeldas[13].ChildNodes[0].InnerText;
                                    el.FechaProcesoCancelacion = listaCeldas[14].ChildNodes[0].InnerText;
                                    el.FolioFiscal = celdaDatos.SelectSingleNode("//input[@id='ListaFolios']")?.GetAttributeValue("value", "");
                                    el.Vigente = el.EstadoComprobante.Equals("Vigente", StringComparison.InvariantCultureIgnoreCase);

                                    if (el.Vigente)
                                    {
                                        el.LinkXML = celdaDatos.SelectSingleNode("//span[@id='BtnDescarga']")?.GetAttributeValue("onclick", "");
                                        el.LinkPDF = celdaDatos.SelectSingleNode("//span[@id='BtnRI']")?.GetAttributeValue("onclick", "");
                                    }
                                    l.Add(el);
                                }
                            }
                        }

                    };

                }



            }

            return l;
        }

        public static Procesamiento ArgProcesamiento(this EstadoProcesamiento e, string Error = null)
        {
            return new Procesamiento(e, Error);
        }

        public static Notificacion ArgNotificacion(this string m, string modulo, TipoNotificacion tipo = TipoNotificacion.Ninguno, string Error = null)
        {
            return new Notificacion(modulo, m, tipo, Error);
        }


        public static StringDictionary ObtieneEstadoReceptor(this string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                return ObtieneValorInput(html, ElementosEstadoReceptor);
            }
            return null;
        }


        public static string ObtieneViewStateInicialReceptor(this string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                int i = html.IndexOf("__VIEWSTATE|");
                if (i >= 0)
                {
                    i = i + "__VIEWSTATE|".Length;
                    int e = html.IndexOf("|", i );
                    if (e >= 0)
                    {
                        string v = html.Substring(i, e - i);
                        return v;
                    }
                }

            }
            return null;
        }

        private static StringDictionary ObtieneValorInput(this string html, List<string> elementos)
        {
            StringDictionary d = new StringDictionary();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            elementos.ForEach(e =>
            {
                string val = null;
                try
                {
                    val = document.DocumentNode.SelectSingleNode($"//input[@name='{e}']").Attributes["value"].Value;
                }
                catch (Exception)
                {

                }
                d.Add(e, val);
            });

            return d;
        }

        public static string ObtienetokeWResultLogin(this string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);
                string r = null;
                try
                {
                    r = document.DocumentNode.SelectSingleNode("//input[@name='wresult']").Attributes["value"].Value;
                }
                catch (Exception)
                {

                }
                return r;
            }
            return null;
        }

        public static string Obtienetokenuuid(this string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);
                return document.DocumentNode.SelectSingleNode("//form[@id='certform']/input[@id='tokenuuid']").Attributes["value"].Value;

            }
            return null;
        }
    }
}
