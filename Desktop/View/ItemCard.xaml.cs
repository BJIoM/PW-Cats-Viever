using System;
using System.Windows;
using System.Windows.Media.Imaging;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop.View {
	public partial class ItemCard {
		#region Свойства

		/// <summary>
		/// Предмет
		/// </summary>
		public Item Item {
			get => (Item) GetValue(ItemProperty);
			set {
				SetValue(ItemProperty, value);
				Item.Updated += OnItemUpdate;
			}
		}

		#endregion

		#region Поля

		private bool _editMode;

		public static readonly DependencyProperty ItemProperty =
			DependencyProperty.Register("Item", typeof(Item), typeof(ItemCard));

		#endregion

		#region Конструктор

		public ItemCard() {
			InitializeComponent();
			_editMode = false;
		}

		#endregion

		#region Обработчики событий

		private void OnItemUpdate(object sender, EventArgs e) {
			ItemLink.Text = Item.Link;
			ItemName.Content = Item.Name;
			ItemBuyPrice.Content = Item.Price.Buy.Highest != 0 ? Item.Price.Buy.Highest.ToString() : "";
			ItemSellPrice.Content = Item.Price.Sell.Lowest != 0 ? Item.Price.Sell.Lowest.ToString() : "";
			ItemIcon.Source = new BitmapImage(new Uri(Item.Icon));
		}



		private void Settings_OnClick(object sender, RoutedEventArgs e) {
			if (!_editMode) {
				_editMode = true;
				ItemLink.Visibility = Visibility.Visible;
				ItemName.Visibility = Visibility.Hidden;
			}
			else {
				_editMode = false;
				ItemLink.Visibility = Visibility.Hidden;
				Item.Link = ItemLink.Text;
				ItemName.Visibility = Visibility.Visible;
			}
		}

		#endregion
	}
}