using System;
using System.IO;
using System.Text.Json;

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
				configuration = JsonSerializer.Deserialize<T>(File.ReadAllText(filepath));
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

			File.WriteAllText(Path.Combine(path, name + _ext_), JsonSerializer.Serialize(configuration));
			return configuration;
		}
	}
}
