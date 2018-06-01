using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PWCatsViewer.Desktop.Annotations;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop {
	public sealed class MainWindowModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;


		/// <summary>
		/// Предметы
		/// </summary>
		public ObservableCollection<Item> Items { get; set; }

		/// <summary>
		/// Выбранный предмет
		/// </summary>
		public Item SelectedItem {
			get => Items[_selectedItem];
		}

		public ICommand Click {
			get => new ClickDelegate((obj) => {
				_selectedItem = Convert.ToInt32(obj);
				SelectedItem.ReCalculate();
				OnPropertyChanged(nameof(SelectedItem));
			});
		}



		private int _selectedItem;



		public MainWindowModel() {
			Items = new ObservableCollection<Item>(new List<Item>(5));
			_selectedItem = 0;
		}



		public void Load() {
			if (File.Exists("items.json")) {
				string json = File.ReadAllText("items.json");
				var items = JsonConvert.DeserializeObject<List<Item>>(json);
				foreach (Item item in items) {
					Items.Add(item);
				}
			}
			else {
				Items.Add(new Item("https://pwcats.info/lisichka/11208"));
				Items.Add(new Item("https://pwcats.info/lisichka/12830"));
				Items.Add(new Item("https://pwcats.info/lisichka/12979"));
				Items.Add(new Item("https://pwcats.info/lisichka/41375"));
				Items.Add(new Item("https://pwcats.info/lisichka/39872"));
			}
		}



		public void Save() {
			string json = JsonConvert.SerializeObject(Items, Formatting.Indented);
			File.WriteAllText("items.json", json);
		}



		[NotifyPropertyChangedInvocator]
		private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}