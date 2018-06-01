using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;

namespace PWCatsViewer.Logic.Data {
	public static class PWCatsApi {
		public static async Task<Price> GetPriceAsync(string link) => await Task.Run(() => GetPrice(link));



		private static Price GetPrice(string link) {
			string html = GetHtml(link);
			IHtmlDocument doc = new HtmlParser().Parse(html);
			IElement table =
				doc.GetElementsByClassName("table table-condensed table-hover table-colored pwcats-items-table")[0];
			var tr = table.GetElementsByClassName("item_tr");
			Price price = new Price();
			foreach (IElement element in tr) {
				if (element.Children[4].TextContent != "") {
					price.Sell = element.Children[4].TextContent;
					break;
				}
				if (element.Children[5].TextContent != "") {
					price.Buy = element.Children[5].TextContent;
				}
			}
			
			return price;
		}

		public static string GetItemName(int id) {
			string html = GetHtml("http://pwdb.info/ru/item/" + id);
			IHtmlDocument doc = new HtmlParser().Parse(html);
			return doc.GetElementsByTagName("h3")[0].TextContent;
		}

		static string GetHtml(string url) {
			string html;
			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			req.CookieContainer = new CookieContainer();
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
			using (StreamReader stream = new StreamReader(
				resp.GetResponseStream(), Encoding.UTF8)) {
				html = stream.ReadToEnd();
			}
			return html;
		}
	}
}