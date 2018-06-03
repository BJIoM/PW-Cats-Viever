namespace PWCatsViewer.Logic.Api.PwCatsApi.Model {
	public class Item {
		#region Свойства

		/// <summary>
		/// Id предмета
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Название предмета
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Цена на премет
		/// </summary>
		public Price Price { get; set; }

		#endregion
	}
}