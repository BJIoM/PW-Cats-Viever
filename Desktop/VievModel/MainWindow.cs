using System.Collections.Generic;
using System.IO;
using System.Timers;
using Newtonsoft.Json;
using PWCatsViewer.Desktop.Properties;
using PWCatsViewer.Logic.Data;

namespace PWCatsViewer.Desktop.VievModel {
	public class MainWindow {
		#region Свойства

		/// <summary>
		/// Список предметов
		/// </summary>
		public List<Item> Items { get; }

		#endregion


		#region Поля

		/// <summary>
		/// Таймер обновления
		/// </summary>
		private Timer _updater;

		#endregion


		#region Конструкторы

		public MainWindow() {
			Items = new List<Item>();
			int itemsCount = Settings.Columns * Settings.Rows;
			for (int i = 0; i < itemsCount; i++) {
				Items.Add(new Item());
			}
		}

		#endregion


		#region Методы

		/// <summary>
		/// Загрузка пердметов
		/// </summary>
		public void Init() {
			var items = Load();
			
			for (int item = 0; item < items.Count; item++) {
				Items[item].Link = items[item].Link;
			}

			_updater = new Timer {
				                     Interval = Settings.UpdateInterval * 1000,
				                     AutoReset = true
			                     };
			_updater.Elapsed += Update;
			_updater.Start();
		}


		/// <summary>
		/// Чтение предметов с диска
		/// </summary>
		/// <returns></returns>
		private static List<Item> Load() {
			var result = new List<Item>();
			if (File.Exists("items.json")) {
				string json = File.ReadAllText("items.json");
				result = JsonConvert.DeserializeObject<List<Item>>(json);
			}

			while (result.Count < Settings.Columns * Settings.Rows) {
				result.Add(new Item());
			}

			return result;
		}

		/// <summary>
		/// Сохранение предметов на диск
		/// </summary>
		public void Save() {
			string json = JsonConvert.SerializeObject(Items, Formatting.Indented);
			File.WriteAllText("items.json", json);
		}



		/// <summary>
		/// Обновление цен на предметы
		/// </summary>
		private void Update(object sender, ElapsedEventArgs e) {
			foreach (Item item in Items) {
				item.UpdatePrice();
			}
		}

		#endregion
	}
}