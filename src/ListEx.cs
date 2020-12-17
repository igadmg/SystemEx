using System;
using System.Collections.Generic;
using System.Linq;

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

		public static bool Remove<T>(this List<T> l, T item, Action<T> executeFn)
		{
			if (l.Remove(item))
			{
				executeFn(item);
				return true;
			}

			return false;
		}

		public static void Add<T>(this ICollection<T> c, IEnumerable<T> e)
		{
			foreach (var i in e)
				c.Add(i);
		}

		public static bool Remove<T>(this ICollection<T> c, IEnumerable<T> e)
		{
			bool result = false;

			foreach (var i in e)
				result |= c.Remove(i);

			return result;
		}

		public static void Update<T>(this IList<T> l, IEnumerable<T> e)
		{
			var nl = e.ToList();

			l.Remove(l.Except(nl).ToList());
			l.Add(nl.Except(l).ToList());
		}
	}
}
