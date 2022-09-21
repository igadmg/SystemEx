using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace SystemEx
{
	public static class ProcessEx
	{
		public static string EscapeArguments(params string[] args)
		{
			return string.Join(" ", args.Select((a) =>
			{
				var s = Regex.Replace(a, @"(\\*)" + "\"", @"$1$1\" + "\"");
				if (s.Contains(" "))
				{
					s = "\"" + Regex.Replace(s, @"(\\+)$", @"$1$1") + "\"";
				}
				return s;
			}).ToArray());
		}

		public static int Command(string command, Action<string> contentFn = null)
		{
			var errorlevelFileName = Path.GetTempFileName();
			using (Process process = new Process())
			{
				process.StartInfo = new ProcessStartInfo {
					UseShellExecute = false,
					RedirectStandardOutput = contentFn != null,
				};
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					process.StartInfo.FileName = "cmd.exe";
					process.StartInfo.Arguments = "/v:on /c \"" + command + $" & echo !errorlevel! > {errorlevelFileName}\"";
				}
				else
				{
					process.StartInfo.FileName = "/bin/bash";
					process.StartInfo.Arguments = command;
				}

				process.Start();
				if (contentFn != null)
				{
					while (true)
					{
						var str = process.StandardOutput.ReadLine();
						if (str == null)
							break;

						contentFn(str);
					}
				}
				process.WaitForExit();
			}

			int errorlevel = 0;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				if (!int.TryParse(File.ReadAllText(errorlevelFileName), out errorlevel))
					errorlevel = -1;
				File.Delete(errorlevelFileName);
			}

			return errorlevel;
		}

		public static int Open(string filename)
		{
			Console.WriteLine("> " + filename);

			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.UseShellExecute = true;
			processStartInfo.WorkingDirectory = Path.GetDirectoryName(filename);
			processStartInfo.FileName = filename;
			processStartInfo.Verb = "OPEN";
			using Process process = Process.Start(processStartInfo);

			return 0;
		}
	}
}
