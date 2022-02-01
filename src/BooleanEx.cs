using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEx
{
	public static class BooleanEx
	{
		public static float To01f(this bool v) => v ? 0f : 1f;
	}
}
