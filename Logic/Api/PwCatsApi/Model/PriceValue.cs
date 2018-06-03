using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PWCatsViewer.Logic.Api.PwCatsApi.Model {
	public class PriceValue {
		#region Свойства

		/// <summary>
		/// Максимальное значение
		/// </summary>
		public int Highest {
			get => _prices.Last();
		}

		/// <summary>
		/// Минимальное значение
		/// </summary>
		public int Lowest {
			get => _prices.First();
		}

		#endregion

		#region Поля

		private readonly List<int> _prices;

		#endregion

		#region Конструкторы

		public PriceValue(IEnumerable<string> prices) {
			_prices = new List<int>();
			foreach (string price in prices) {
				int value = int.Parse(price, NumberStyles.AllowThousands, new CultureInfo("ru-RU"));
				_prices.Add(value);
			}
		}



		public PriceValue() => _prices = new List<int> {0};

		#endregion
	}
}