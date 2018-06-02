using System;
using System.ComponentModel;
using System.Windows;
using PWCatsViewer.Desktop.Properties;

namespace PWCatsViewer.Desktop {
	public partial class SettingsWindow {
		public SettingsWindow() => InitializeComponent();



		private void Rows_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e) =>
			Settings.Rows = Convert.ToInt32(e.NewValue);



		private void Columns_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e) =>
			Settings.Columns = Convert.ToInt32(e.NewValue);



		private void MetroWindow_OnClosing(object sender, CancelEventArgs e) => Settings.Save();



		private void UpdateInterval_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e) =>
			Settings.UpdateInterval = Convert.ToInt32(e.NewValue);
	}
}