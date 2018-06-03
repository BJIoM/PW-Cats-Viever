using System;
using Newtonsoft.Json;
using PWCatsViewer.Logic.Api.PwCatsApi;
using PWCatsViewer.Logic.Api.PwCatsApi.Model;

namespace PWCatsViewer.Logic.Data {
	/// <summary>
	/// Предмет
	/// </summary>
	public class Item {
		#region События

		public event EventHandler Updated;

		#endregion
		
		#region Свойства

		/// <summary>
		/// Название предмета
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Цена
		/// </summary>
		[JsonIgnore]
		public Price Price { get; private set; }

		/// <summary>
		/// Ссылка на предмет
		/// </summary>
		public string Link {
			get => _link;
			set {
				_link = value;
				Update();
			}
		}

		/// <summary>
		/// Иконка предмета
		/// </summary>
		[JsonIgnore]
		public string Icon {
			get => $"https://pwcats.info/img/item/{Id}.png";
		}

		/// <summary>
		/// Id предмета
		/// </summary>
		[JsonIgnore]
		private int Id { get; set; }

		#endregion

		#region Поля

		private string _link;

		#endregion

		#region Конструкторы

		public Item() => _link = "";

		#endregion

		#region Методы

		public void UpdatePrice() {
			if (Link != "") {
				Price = PwCatsApi.Items.GetPriceByLink(Link);
				Updated?.Invoke(this, EventArgs.Empty);
			}
		}



		private void Update() {
			if (Link != "") {
				Api.PwCatsApi.Model.Item item = PwCatsApi.Items.GetItemByLink(Link);
				Id = item.Id;
				Name = item.Name;
				Price = item.Price;
				Updated?.Invoke(this, EventArgs.Empty);
			}
			else {
				Id = 0;
				Name = "";
				Price = new Price();
				Updated?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion
	}
}