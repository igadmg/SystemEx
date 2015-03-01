using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SystemEx
{
	public static class StreamReaderEx
	{
		public static IEnumerable<string> EnumerateStrings(this StreamReader sr)
		{
			while (true) {
				var line = sr.ReadLine();
				if (line != null)
					yield return line;
				else
					yield break;
			}
		}
	}
}
