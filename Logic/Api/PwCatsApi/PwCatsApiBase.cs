using System.IO;
using System.Net;
using System.Text;

namespace PWCatsViewer.Logic.Api.PwCatsApi {
	/// <summary>
	/// Общий класс для работы с pwcats.info
	/// </summary>
	public class PwCatsApiBase {
		/// <summary>
		/// Получает HTML-страницу находящуюся по адресу
		/// </summary>
		/// <param name="url">Адрес страницы</param>
		/// <returns>HTML-страница</returns>
		protected static string GetHtml(string url) {
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