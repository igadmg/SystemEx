using System.IO;

namespace SystemEx
{
	public static class DirectoryEx
	{
		public static void Copy(string sourceDirName, string destDirName)
		{
			Directory.CreateDirectory(destDirName);
			File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));

			foreach (string dir in Directory.GetDirectories(sourceDirName, "*", SearchOption.AllDirectories))
			{
				var newDir = dir.Replace(sourceDirName, destDirName);
				Directory.CreateDirectory(newDir);
				File.SetAttributes(newDir, File.GetAttributes(dir));
			}

			foreach (string file in Directory.GetFiles(sourceDirName, "*", SearchOption.AllDirectories))
			{
				var newFile = file.Replace(sourceDirName, destDirName);
				File.Copy(file, newFile, true);
				File.SetAttributes(newFile, File.GetAttributes(file));
			}
		}
	}
}
