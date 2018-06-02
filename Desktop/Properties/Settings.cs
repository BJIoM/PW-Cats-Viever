using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;

namespace PWCatsViewer.Desktop.Properties {
	public static class Settings {
		public static int Rows {
			get => Convert.ToInt32(_settings["Rows"]);
			set => _settings["Rows"] = value.ToString();
		}

		public static int Columns {
			get => Convert.ToInt32(_settings["Columns"]);
			set => _settings["Columns"] = value.ToString();
		}

		public static int UpdateInterval {
			get => Convert.ToInt32(_settings["UpdateInterval"]);
			set => _settings["UpdateInterval"] = value.ToString();
		}

		private static readonly Dictionary<string, string> _settings;



		static Settings() {
			if (File.Exists("settings.json")) {
				string json = File.ReadAllText("settings.json");
				_settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
			}
			else {
				_settings = new Dictionary<string, string>() {
					                                             {"Rows", "3"},
					                                             {"Columns", "3"},
					                                             {"UpdateInterval", "10"},
				                                             };
			}
		}



		public static void Save() {
			string json = JsonConvert.SerializeObject(_settings, Formatting.Indented);
			File.WriteAllText("settings.json", json);
		}
	}
}