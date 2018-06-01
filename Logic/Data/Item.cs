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
		/// Сервер
		/// </summary>
		public string Server {
			get => _server;
			set {
				_server = value;
				OnPropertyChanged(nameof(Server));
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
		public string Link { get; set; }

		private string _name;
		private string _server;
		private Price _price;

        

		/// <summary>
		/// Предмет
		/// </summary>
		/// <param name="link">Ссылка на котобазу</param>
		public Item(string link) {
			Link = link;

			Regex parser = new Regex(@"pwcats.info\/(\w*)\/(\d*)");
			Match info = parser.Match(Link);
			int id = Convert.ToInt32(info.Groups[2].Value);

			Server = info.Groups[1].ToString();
			Name = PWCatsApi.GetItemName(id);
			Price = new Price();
		}



		public async void ReCalculate() => Price = await PWCatsApi.GetPriceAsync(Link);



		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}