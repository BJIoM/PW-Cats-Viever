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

namespace PWCatsViewer.Desktop.View {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		private VievModel.MainWindow Data { get; }



		public MainWindow() {
			InitializeComponent();

			Data = new VievModel.MainWindow();

			ItemTable.Rows = Settings.Rows;
			ItemTable.Columns = Settings.Columns;

			int index = 0;
			for (int row = 0; row < Settings.Rows; row++) {
				for (int column = 0; column < Settings.Columns; column++) {
					Data.Items.Add(new Item());
					ItemCard card = new ItemCard {
						                             Item = Data.Items[index++],
						                             Style = GetStyle(row, column)
					                             };
					ItemTable.Children.Add(card);
					Grid.SetRow(card, row);
					Grid.SetColumn(card, column);
				}
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



		private async void Window_OnLoaded(object sender, RoutedEventArgs e) {
			Title = "PW Cats Viewer [Загрузка...]";
			Data.Init();
			Title = "PW Cats Viewer";
		}



		private void Window_OnClosed(object sender, EventArgs e) => Data.Save();



		private void SettingsButton_OnClick(object sender, RoutedEventArgs e) {
			SettingsWindow settings = new SettingsWindow();
			settings.ShowDialog();
			Assembly app = Assembly.GetEntryAssembly();
			System.Diagnostics.Process.Start($"{app.Location}");
			Application.Current.Shutdown();
		}
	}
}