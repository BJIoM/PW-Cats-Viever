using System.Windows;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop {
	public partial class ItemCard {
		public Item Item {
			get => (Item) GetValue(ItemProperty);
			set => SetValue(ItemProperty, value);
		}


		public ItemCard() => InitializeComponent();


		public static readonly DependencyProperty ItemProperty =
			DependencyProperty.Register(
				"Item", typeof(Item), typeof(ItemCard));
	}
}