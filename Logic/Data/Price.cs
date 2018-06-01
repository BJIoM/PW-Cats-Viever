using System.ComponentModel;
using System.Runtime.CompilerServices;
using PWCatsViewer.Logic.Annotations;

namespace PWCatsViewer.Logic.Data {
	/// <summary>
	/// Цена
	/// </summary>
	public class Price : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Покупка
		/// </summary>
		public string Buy {
			get => _buy;
			set {
				_buy = value;
				OnPropertyChanged(nameof(Buy));
			}
		}

		/// <summary>
		/// Продажа
		/// </summary>
		public string Sell {
			get => _sell;
			set {
				_sell = value;
				OnPropertyChanged(nameof(Sell));
			}
		}

		private string _sell;
		private string _buy;



		public Price(string sell = "", string buy = "") {
			Sell = sell;
			Buy = buy;
		}



		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}