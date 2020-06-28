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

		public static int BinarySearch<T>(this List<T> list, T item, Comparison<T> comparer)
		{
			return list.BinarySearch(item, new LambdaComparer<T>(comparer));
		}

		//public static int BinarySearch<T, U>(this List<T> list, U item, Comparison<T, U> comparer)
		//{
		//	return list.BinarySearch(default, new LambdaComparer<T>((a, b) => comparer(a, item)));
		//}

		public static int BinarySearch<T>(this List<T> list, int index, int count, T item, Comparison<T> comparer)
		{
			return list.BinarySearch(index, count, item, new LambdaComparer<T>(comparer));
		}

		public static void Clear<T>(this IList<T> list, Action<T> disposeFn)
		{
			foreach (var item in list)
				disposeFn(item);
			list.Clear();
		}
	}
}
