using System.Windows;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop {
	public partial class ItemCard {
		public Item Item {
			get => (Item) GetValue(ItemProperty);
			set => SetValue(ItemProperty, value);
		}

		private bool editMode;
		
		public ItemCard() {
			InitializeComponent();
			editMode = false;
		}



		public static readonly DependencyProperty ItemProperty =
			DependencyProperty.Register(
				"Item", typeof(Item), typeof(ItemCard));


		private void Button_OnClick(object sender, RoutedEventArgs e) {
			if (!editMode) {
				editMode = true;
				ItemLink.Visibility = Visibility.Visible;
			}
			else {
				editMode = false;
				ItemLink.Visibility = Visibility.Hidden;
			}
		}
	}
}