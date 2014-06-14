using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemEx
{
	public static class StringEx
	{
		public static string format(this string s, params object[] args)
		{
			return string.Format(s, args);
		}
	}
}
