using System;
using System.IO;
#if USE_TEXT_JSON
using System.Text.Json;
#endif
#if UNITY || UNITY_64
using Newtonsoft.Json;
#endif

namespace SystemEx
{
	public class AppConfiguration : IDisposable
	{
		public static string _default_ = "default";
		public static string _ext_ = ".json.cfg";
		public string AppName { get; protected set; }
		public string ConfigurationFolderPath
			=> Path.Combine(
				Environment.GetFolderPath(
					Environment.SpecialFolder.LocalApplicationData)
				, AppName);

		public AppConfiguration(string appName)
		{
			AppName = appName;
		}

		public void Dispose() {}

		public bool LoadConfiguration<T>(ref T configuration)
			=> LoadConfiguration<T>(_default_, ref configuration, ConfigurationFolderPath);

		private bool LoadConfiguration<T>(string name, ref T configuration, string path)
		{
			var filepath = Path.Combine(path, name + _ext_);
			if (File.Exists(filepath))
			{
#if USE_TEXT_JSON
				configuration = JsonSerializer.Deserialize<T>(File.ReadAllText(filepath));
#endif
#if UNITY || UNITY_64
				configuration = JsonConvert.DeserializeObject<T>(File.ReadAllText(filepath));
#endif
				return true;
			}

			return false;
		}
		

		public T SaveConfiguration<T>(T configuration)
			=> SaveConfiguration(_default_, configuration, ConfigurationFolderPath);

		public T SaveConfiguration<T>(string name, T configuration)
			=> SaveConfiguration(name, configuration, ConfigurationFolderPath);

		public T SaveConfiguration<T>(string name, T configuration, string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

#if USE_TEXT_JSON
			File.WriteAllText(Path.Combine(path, name + _ext_), JsonSerializer.Serialize(configuration));
#endif
#if UNITY || UNITY_64
			File.WriteAllText(Path.Combine(path, name + _ext_), JsonConvert.SerializeObject(configuration));
#endif
			return configuration;
		}
	}
}
