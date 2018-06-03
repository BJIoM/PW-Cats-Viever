namespace PWCatsViewer.Logic.Api.PwCatsApi.Model {
	/// <summary>
	/// Цена
	/// </summary>
	public class Price {
		#region Свойства

		/// <summary>
		/// Цена продажи
		/// </summary>
		public PriceValue Sell { get; set; }

		/// <summary>
		/// Цена покупки
		/// </summary>
		public PriceValue Buy { get; set; }

		#endregion

		#region Конструкторы

		public Price() {
			Sell = new PriceValue();
			Buy = new PriceValue();
		}

		#endregion
	}
}