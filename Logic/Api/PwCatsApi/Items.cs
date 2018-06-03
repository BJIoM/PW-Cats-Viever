using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using PWCatsViewer.Logic.Api.PwCatsApi.Model;

namespace PWCatsViewer.Logic.Api.PwCatsApi {
	/// <summary>
	/// Методы для работы с предметами
	/// </summary>
	public class Items : PwCatsApiBase {
		#region Методы

		/// <summary>
		/// Информация о предмете по ссылке
		/// </summary>
		/// <param name="link">Ссылка на предмет</param>
		/// <returns>Информация о предмете</returns>
		public Item GetItemByLink(string link) {
			Regex parser = new Regex(@"pwcats.info\/(\w*)\/(\d*)");
			Match info = parser.Match(link);
			int id = Convert.ToInt32(info.Groups[2].Value);

			return new Item {
				                Id = id,
				                Name = GetNameById(id),
				                Price = GetPriceByLink(link)
			                };
		}



		/// <summary>
		/// Название предмета по ID
		/// </summary>
		/// <param name="id">ID предмета</param>
		/// <returns>Русское название предмета</returns>
		public string GetNameById(int id) {
			string html = GetHtml("http://pwdb.info/ru/item/" + id);
			IHtmlDocument doc = new HtmlParser().Parse(html);
			return doc.GetElementsByTagName("h3")[0].TextContent;
		}



		/// <summary>
		/// Цены на предмет по ссылке
		/// </summary>
		/// <param name="link"></param>
		/// <returns>Цены на предмет</returns>
		public Price GetPriceByLink(string link) {
			string html = GetHtml(link);
			IHtmlDocument doc = new HtmlParser().Parse(html);
			IElement table =
				doc.GetElementsByClassName("table table-condensed table-hover table-colored pwcats-items-table")[0];
			var tr = table.GetElementsByClassName("item_tr");

			var sell = new List<string>();
			var buy = new List<string>();
			foreach (IElement element in tr) {
				if (element.Children[4].TextContent != "") {
					sell.Add(element.Children[4].TextContent);
				}

				if (element.Children[5].TextContent != "") {
					buy.Add(element.Children[5].TextContent);
				}
			}

			return new Price {
				                 Sell = new PriceValue(sell),
				                 Buy = new PriceValue(buy)
			                 };
		}

		#endregion


		#region Ассинхронные методы

		/// <inheritdoc cref="GetItemByLink"/>
		public async Task<Item> GetItemByLinkAsync(string link) =>
			await Task.Run(() => GetItemByLink(link));



		/// <inheritdoc cref="GetNameById"/>
		public async Task<string> GetNameByIdAsync(int id) =>
			await Task.Run(() => GetNameById(id));



		/// <inheritdoc cref="GetPriceByLink"/>
		public async Task<Price> GetPriceByLinkAsync(string link) =>
			await Task.Run(() => GetPriceByLink(link));

		#endregion
	}
}