using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace satbot.poller
{
	public class valoresPost
	{
		public List<parPost> pares;

		public valoresPost()
		{
			this.pares = new List<parPost>();
		}

		public void addPar(string clave, string valor)
		{
			parPost p = new parPost()
			{
				clave = clave,
				valor = valor
			};
			this.pares.Add(p);
			p = null;
		}

		public void addPares(List<parPost> pares)
		{
			this.pares.AddRange(pares);
			
		}

		public string obtienePostString()
		{
			string r = "";
			foreach (parPost p in this.pares)
			{
				r = (r != "" ? string.Concat(new string[] { r, "&", HttpUtility.UrlEncode(p.clave), "=", HttpUtility.UrlEncode(HttpUtility.HtmlDecode(p.valor)) }) : string.Concat(HttpUtility.UrlEncode(p.clave), "=", HttpUtility.UrlEncode(HttpUtility.HtmlDecode(p.valor))));
			}
			return r;
		}
	}

	public class parPost
	{
		private string _clave;

		private string _valor;

		public string clave
		{
			get
			{
				return this._clave;
			}
			set
			{
				this._clave = value;
			}
		}

		public string valor
		{
			get
			{
				return this._valor;
			}
			set
			{
				this._valor = value;
			}
		}

		public parPost()
		{
			this._valor = "";
			this._clave = "";
		}
	}
}
