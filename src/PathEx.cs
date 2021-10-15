using System.IO;

namespace SystemEx
{
	public static class PathEx
	{
		public static string Combine(char separator, params string[] paths)
			=> Path.Combine(paths).Replace(Path.DirectorySeparatorChar, separator);
	}
}
