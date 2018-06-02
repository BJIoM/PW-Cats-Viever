using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		/// <summary>
		/// Предметы
		/// </summary>
		public ObservableCollection<Item> Items { get; }

		private Timer _updater;



		public MainWindow() {
			InitializeComponent();

			Items = new ObservableCollection<Item>();

			for (int row = 0; row < 3; row++) {
				for (int column = 0; column < 3; column++) {
					ItemCard card = new ItemCard {
						                             Margin = new Thickness(1),
						                             Style = GetStyle(row, column)
					                             };
					ItemTable.Children.Add(card);
					Grid.SetRow(card, row);
					Grid.SetColumn(card, column);
				}
			}
		}



		private async void Window_OnLoaded(object sender, RoutedEventArgs e) {
			Title = "PW Cats Viewer [Загрузка...]";

			var items = await Task.Run(() => {
				if (File.Exists("items.json")) {
					string json = File.ReadAllText("items.json");
					return JsonConvert.DeserializeObject<List<Item>>(json);
				}
				else {
					return new List<Item> {
						                      new Item("https://pwcats.info/lisichka/11208"),
						                      new Item("https://pwcats.info/lisichka/12830"),
						                      new Item("https://pwcats.info/lisichka/12979"),
						                      new Item("https://pwcats.info/lisichka/41375"),
						                      new Item("https://pwcats.info/lisichka/39872"),
						                      new Item("https://pwcats.info/lisichka/24721"),
						                      new Item("https://pwcats.info/lisichka/25821"),
						                      new Item("https://pwcats.info/lisichka/25820"),
						                      new Item("https://pwcats.info/lisichka/50249"),
					                      };
				}
			});
			for (int i = 0; i < items.Count; i++) {
				Items.Add(items[i]);
				Items[i].UpdatePrice();
				( (ItemCard) ItemTable.Children[i] ).Item = Items[i];
			}

			_updater = new Timer {
				                     Interval = 60000,
				                     AutoReset = true,
			                     };
			_updater.Elapsed += UpdatePrices;
			_updater.Start();

			Title = "PW Cats Viewer";
		}



		private void UpdatePrices(object sender, ElapsedEventArgs e) {
			foreach (Item item in Items) {
				item.UpdatePrice();
			}
		}


		private Style GetStyle(int row, int column) {
			int pos = ( ( row + 1 ) + ( column + 1 ) ) % 2;
			
			Style itemCardStyle = new Style();
			itemCardStyle.Setters.Add(new Setter {Property = BackgroundProperty, Value = pos == 0 ? Brushes.Bisque : Brushes.PapayaWhip});
			return itemCardStyle;
		}



		private void Window_OnClosed(object sender, EventArgs e) {
			string json = JsonConvert.SerializeObject(Items, Formatting.Indented);
			File.WriteAllText("items.json", json);
		}
	}
}