using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;
using PWCatsViewer.Desktop.Properties;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		/// <summary>
		/// Предметы
		/// </summary>
		private ObservableCollection<Item> Items { get; }

		private Timer _updater;



		public MainWindow() {
			InitializeComponent();

			Items = new ObservableCollection<Item>();

			ItemTable.Rows = Settings.Rows;
			ItemTable.Columns = Settings.Columns;

			for (int row = 0; row < Settings.Rows; row++) {
				for (int column = 0; column < Settings.Columns; column++) {
					Items.Add(new Item());
					ItemCard card = new ItemCard {Style = GetStyle(row, column)};
					ItemTable.Children.Add(card);
					Grid.SetRow(card, row);
					Grid.SetColumn(card, column);
				}
			}
		}



		private async void Window_OnLoaded(object sender, RoutedEventArgs e) {
			Title = "PW Cats Viewer [Загрузка...]";

			var items = await Task.Run(() => {
				var result = new List<Item>();
				if (File.Exists("items.json")) {
					string json = File.ReadAllText("items.json");
					result = JsonConvert.DeserializeObject<List<Item>>(json);
				}

				if (result.Count < Settings.Columns * Settings.Rows) {
					result.Add(new Item());
				}

				return result;
			});
			for (int i = 0; i < items.Count; i++) {
				Items[i].Link = items[i].Link;
				Items[i].UpdatePrice();
				try {
					( (ItemCard) ItemTable.Children[i] ).Item = Items[i];
				}
				catch {
					break;
				}
			}

			_updater = new Timer {
				                     Interval = Settings.UpdateInterval * 1000,
				                     AutoReset = true
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



		private static Style GetStyle(int row, int column) {
			int pos = ( ( row + 1 ) + ( column + 1 ) ) % 2;

			Style itemCardStyle = new Style();
			itemCardStyle.Setters.Add(new Setter {
				                                     Property = BackgroundProperty,
				                                     Value = pos == 0 ? Brushes.Bisque : Brushes.PapayaWhip
			                                     });
			itemCardStyle.Setters.Add(new Setter {Property = MarginProperty, Value = new Thickness(1)});
			itemCardStyle.Setters.Add(new Setter {Property = MaxHeightProperty, Value = (double) 120});
			itemCardStyle.Setters.Add(new Setter {Property = MaxWidthProperty, Value = (double) 300});
			
			return itemCardStyle;
		}



		private void Window_OnClosed(object sender, EventArgs e) {
			string json = JsonConvert.SerializeObject(Items, Formatting.Indented);
			File.WriteAllText("items.json", json);
		}



		private void Button_OnClick(object sender, RoutedEventArgs e) {
			SettingsWindow settings = new SettingsWindow();
			settings.ShowDialog();
			Assembly app = Assembly.GetEntryAssembly();
			System.Diagnostics.Process.Start($"{app.Location}");
			Application.Current.Shutdown();
		}
	}
}