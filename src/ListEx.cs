using System;
using System.Collections.Generic;

namespace SystemEx
{
	public static class ListEx
	{
		public static int Reserve<T>(this List<T> list, int size)
		{
			list.Capacity = Math.Max(list.Capacity, size);
			return list.Capacity;
		}
	}
}
