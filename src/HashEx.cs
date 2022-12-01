using System.Security.Cryptography;
using System.Text;

namespace SystemEx
{
	public static class HashEx
	{
		public static string HashSHA1(this string str, string format = "x2")
		{
			var sha1 = new SHA1Managed();

			var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
			var sb = new StringBuilder(hash.Length * 2);

			foreach (byte b in hash)
			{
				sb.Append(b.ToString(format));
			}

			return sb.ToString();
		}
	}
}
