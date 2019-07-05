using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemEx
{
	public static class StringEx
	{
        /// <summary>
        /// Foramat string as a member function of string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string format(this string str, params object[] args)
		{
            return String.Format(str, args);
		}

		public static char at(this string s, int i)
		{
			if (i < 0)
				return s[s.Length + i];
			return s[i];
		}

		public static string Join(this string[] s, string separator)
		{
			return string.Join(separator, s);
		}

		public static string Join(this IEnumerable<string> s, string separator)
		{
			return string.Join(separator, s.ToArray());
		}

		public static string FirstCharacterToLower(this string str)
		{
			if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
				return str;

			return Char.ToLowerInvariant(str[0]).ToString() + str.Substring(1);
		}

		/// <summary>
		/// Convert hexstring to byte array.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static byte[] ToByteArrayHex(this String str)
		{
			int NumberChars = str.Length;
			byte[] bytes = new byte[NumberChars / 2];
			for (int i = 0; i < NumberChars; i += 2)
				bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);

			return bytes;
		}

		/// <summary>
		/// Convert byte array to hex string.
		/// </summary>
		/// <param name="ba"></param>
		/// <returns></returns>
		public static string ToStringHex(this byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);

			return hex.ToString();
		}

		public static bool IsAnyOf(this char ch, params char[] chars)
		{
			foreach (char c in chars)
			{
				if (ch == c)
					return true;
			}

			return false;
		}

		public static object ToAnyType(this string str, params Type[] types)
		{
			foreach (var type in types)
			{
				try
				{
					return Convert.ChangeType(str, type);
				}
				catch { }
			}

			return str;
		}

		public static int SkipWhiteSpace(this string str, int index = 0)
		{
			while (index < str.Length && char.IsWhiteSpace(str[index]))
				index++;

			return index;
		}
	}
}
