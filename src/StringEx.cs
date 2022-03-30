using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemEx
{
	public static class StringEx
	{
		[Obsolete("Use IsNullOrEmpty instead")]
		public static bool null_(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		[Obsolete("Use IsNullOrWhiteSpace instead")]
		public static bool null_ws_(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

		public static bool IsNullOrEmpty(this string str)
			=> string.IsNullOrEmpty(str);

		public static bool IsNullOrWhiteSpace(this string str)
			=> string.IsNullOrWhiteSpace(str);

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

		public static string format(this string str, IFormatProvider format, params object[] args)
		{
			return String.Format(format, str, args);
		}

		public static string format(this string str, Func<string, string> fn)
		{
			var t = str.tokenize();

			StringBuilder sb = new StringBuilder(str.Length);
			while (t.find_any('{'))
			{
				sb.Append(t.token());

				t.step();
				if (!t.find_any('}'))
					throw new FormatException("Missing cloasing '}'");

				sb.Append(fn(t.token()));
				t.step();
			}
			sb.Append(t.token());

			return sb.ToString();
		}

		public static char at(this string s, int i)
		{
			if (i < 0)
				return s[s.Length + i];
			return s[i];
		}

		public static string Join(this string[] s, char separator)
		{
			return string.Join(new string(separator, 1), s);
		}

		public static string Join(this string[] s, string separator)
		{
			return string.Join(separator, s);
		}

		public static string Join(this IEnumerable<string> s, char separator)
		{
			return string.Join(new string(separator, 1), s.ToArray());
		}

		public static string Join(this IEnumerable<string> s, string separator)
		{
			return string.Join(separator, s.ToArray());
		}

		public static string CutEnd(this string s, int length)
		{
			return s.Substring(0, MathOperationsInt.max(s.Length - length, 0));
		}

		public static string CutFront(this string s, char c)
		{
			var ui = s.IndexOf(c);
			if (ui < 0)
				return s;

			return s.Substring(ui + 1);
		}

		public static string CutEnd(this string s, char c)
		{
			var ui = s.LastIndexOf(c);
			if (ui < 0)
				return s;

			return s.CutEnd(s.Length - ui);
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

		public static Dictionary<string, string> ToDictionary(this string str, char separator)
		{
			var a = str.Split(separator);
			Dictionary<string, string> r = new Dictionary<string, string>(a.Length / 2);

			for (int i = 0; i < a.Length;)
			{
				var k = a[i++];
				var v = a[i++];
				r.Add(k, v);
			}

			return r;
		}

		public static string[] ToPath(this string str)
			=> str.Split('/', '\'');

		public static string FromPath(this string[] path)
			=> path.Join('/');

		public static bool IsEmptyPath(this string[] path)
			=> path.Length == 0 || path.All(s => s.IsNullOrWhiteSpace());

		public static int SkipWhiteSpace(this string str, int index = 0)
		{
			while (index < str.Length && char.IsWhiteSpace(str[index]))
				index++;

			return index;
		}

		public static int SkipWhiteSpaceReverse(this string str)
			=> str.SkipWhiteSpaceReverse(str.Length);

		public static int SkipWhiteSpaceReverse(this string str, int index)
		{
			while (index >= 0 && char.IsWhiteSpace(str[index]))
				index--;

			return index;
		}

		public static int IndexOfReverse(this string str, int startIndex, char ch)
		{
			for (int i = startIndex; i >= 0; i--)
			{
				if (str[i] == ch)
					return i;
			}

			return -1;
		}

		public static int IndexOfAnyReverse(this string str, int startIndex, params char[] ch)
		{
			for (int i = startIndex; i >= 0; i--)
			{
				if (str[i].IsAnyOf(ch))
					return i;
			}

			return -1;
		}

		public static LineTokenizer tokenize(this string str)
		{
			return new LineTokenizer {
				line = str
			};
		}

		public static LineTokenizer tokenize(this string str, int si)
		{
			return new LineTokenizer {
				line = str,
				li = si,
				ei = si
			};
		}

		public static bool find_any(this string str, out LineTokenizer tokenizer, out char ch, params char[] chars)
		{
			tokenizer = str.tokenize();
			return tokenizer.find_any(out ch, chars);
		}

		public static bool skip_whitespace(this string str, out LineTokenizer tokenizer, out char ch)
		{
			tokenizer = str.tokenize();
			return tokenizer.skip_whitespace(out ch);
		}

		public static LineCutter cut(this string str)
		{
			return new LineCutter { result = str };
		}
	}

	public class LineTokenizer
	{
		public int li = 0;
		public int ei = 0;

		public string line;


		public LineTokenizer copy()
			=> new LineTokenizer { li = li, ei = ei, line = line };

		public bool begin()
		{
			return ei == -1;
		}

		public bool end()
		{
			return ei >= line.Length;
		}

		public string token()
		{
			return end() ? line.Substring(li) : 
				begin() ? line.Substring(0, li - 1) :
				(ei > li ? line.Substring(li, ei - li) : line.Substring(ei, li - ei));
		}

		public bool find_any(params char[] chars)
		{
			li = ei;
			ei = line.IndexOfAny(chars, li);
			if (ei == -1) ei = line.Length;

			return !end();
		}

		public bool find_any(out char ch, params char[] chars)
		{
			ch = char.MaxValue;
			if (find_any(chars))
				ch = line[ei];

			return !end();
		}

		public bool find_any_reverse(params char[] chars)
		{
			li = ei;
			ei = line.IndexOfAnyReverse(li, chars);

			return !begin();
		}

		public bool find_any_reverse(out char ch, params char[] chars)
		{
			ch = char.MaxValue;
			if (find_any_reverse(chars))
				ch = line[ei];

			return !begin();
		}

		public bool skip_whitespace()
		{
			li = ei;
			ei = line.SkipWhiteSpace(li);

			return !end();
		}

		public bool skip_whitespace(out char ch)
		{
			ch = char.MaxValue;
			if (skip_whitespace())
				ch = line[ei];

			return !end();
		}

		public bool skip_whitespace(out string token)
		{
			token = string.Empty;
			if (skip_whitespace())
				 token = this.token();

			return !end();
		}

		public bool skip_whitespace_reverse()
		{
			li = ei;
			ei = line.SkipWhiteSpaceReverse(li);

			return !end();
		}

		public bool skip_whitespace_reverse(out char ch)
		{
			ch = char.MaxValue;
			if (skip_whitespace())
				ch = line[ei];

			return !end();
		}

		public bool step(int i = 1)
		{
			li = ei;
			ei += i;

			return !end();
		}
	}

	public class LineCutter
	{
		public string result;
		public int di = 0;

		public LineCutter cut(int si, int ei)
		{
			si = si + di;
			ei = ei + di;
			di += 0 - (ei - si);

			result = result
					.Remove(si, ei - si);

			return this;
		}

		public LineCutter cut(int si)
		{
			si = si + di;
			int l = result.Length - si;
			di += 0 - l;

			result = result
					.Remove(si, l);

			return this;
		}

		public LineCutter replace(int si, int ei, string str)
		{
			si = si + di;
			ei = ei + di;
			di += str.Length - (ei - si);

			result = result
					.Remove(si, ei - si)
					.Insert(si, str);

			return this;
		}

		public static implicit operator string(LineCutter lc)
		{
			return lc.result;
		}
	}
}
