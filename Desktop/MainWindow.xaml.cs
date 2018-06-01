using System;
using System.Threading.Tasks;
using System.Windows;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		public MainWindow() => InitializeComponent();



		private async void Window_OnLoaded(object sender, RoutedEventArgs e) {
			if (DataContext is MainWindowModel model) {
				model.Load();
			}
		}



		private void Window_OnClosed(object sender, EventArgs e) {
			if (DataContext is MainWindowModel mainWindowModel) {
				mainWindowModel.Save();
			}
		}
	}
}