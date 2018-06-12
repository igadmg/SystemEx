using System;
using System.Collections.Generic;

namespace SystemEx
{
	public static class ListEx
	{
		public static T Pop<T>(this List<T> list)
		{
			T t = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			return t;
		}

		public static int Reserve<T>(this List<T> list, int size)
		{
			list.Capacity = Math.Max(list.Capacity, size);
			return list.Capacity;
		}
	}
}
