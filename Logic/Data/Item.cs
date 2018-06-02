using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PWCatsViewer.Logic.Annotations;

namespace PWCatsViewer.Logic.Data {
	/// <summary>
	/// Предмет
	/// </summary>
	public class Item : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Название предмета
		/// </summary>
		public string Name {
			get => _name;
			set {
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		/// <summary>
		/// Цена
		/// </summary>
		public Price Price {
			get => _price;
			set {
				_price = value;
				OnPropertyChanged(nameof(Price));
			}
		}

		/// <summary>
		/// Ссылка на предмет
		/// </summary>
		public string Link {
			get => _link;
			set {
				_link = value;
				UpdateName();
				UpdatePrice();
			} }

		private string _name;
		private Price _price;
		private string _link;



		/// <summary>
		/// Предмет
		/// </summary>
		/// <param name="link">Ссылка на котобазу</param>
		public Item(string link) => Link = link;



		public Item() => Link = "";



		private async void UpdateName() {
			if (Link != "") {
				Regex parser = new Regex(@"pwcats.info\/(\w*)\/(\d*)");
				Match info = parser.Match(Link);
				int id = Convert.ToInt32(info.Groups[2].Value);
				Name = await PWCatsApi.GetNameAsync(id);
			}
			else {
				Name = "";
			}
		}
		

		public async void UpdatePrice() {
			if (Link != "") {
				Price = await PWCatsApi.GetPriceAsync(Link);
			}
			else {
				Price = new Price();
			}
		}



		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}