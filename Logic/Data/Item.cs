using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using PWCatsViewer.Logic.Annotations;

namespace PWCatsViewer.Logic.Data {
	/// <summary>
	/// Предмет
	/// </summary>
	public sealed class Item : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Название предмета
		/// </summary>
		[JsonIgnore]
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
		[JsonIgnore]
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
				OnPropertyChanged(nameof(Icon));
				UpdatePrice();
			}
		}

		/// <summary>
		/// Иконка предмета
		/// </summary>
		[JsonIgnore]
		public string Icon {
			get => $"https://pwcats.info/img/item/{_id}.png";
		}


		private string _name;
		private Price _price;
		private string _link;
		private int _id;


		public Item() => Link = "";



		private async void UpdateName() {
			if (Link != "") {
				Regex parser = new Regex(@"pwcats.info\/(\w*)\/(\d*)");
				Match info = parser.Match(Link);
				_id = Convert.ToInt32(info.Groups[2].Value);
				Name = await PWCatsApi.GetNameAsync(_id);
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
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}