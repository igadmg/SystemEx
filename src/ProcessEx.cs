using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System;

namespace SystemEx
{
	public static class ProcessEx
	{
		public static int Command(string command)
		{
			var errorlevelFileName = Path.GetTempFileName();
			ProcessStartInfo processStartInfo = new ProcessStartInfo {
				UseShellExecute = false,
				CreateNoWindow = true,
			};
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				processStartInfo.FileName = "cmd.exe";
				processStartInfo.Arguments = "/v:on /c \"" + command + $" & echo !errorlevel! > {errorlevelFileName}\"";
			}
			else
			{
				processStartInfo.FileName = "/bin/bash";
				processStartInfo.Arguments = command;
			}

			using (Process process = Process.Start(processStartInfo))
			{
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
	}
}
