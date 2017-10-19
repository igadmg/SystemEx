using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace SystemEx
{
	public static class EnumerableEx
	{
		public static IEnumerable<T> convert<T>(this IEnumerable e)
		{
			foreach (object o in e)
				yield return (T)o;

			yield break;
		}

		[Obsolete("Use Linq .Select instead.")]
		public static IEnumerable<U> transform<T, U>(this IEnumerable<T> e, Func<T, U> trf)
		{
			foreach (var i in e)
				yield return trf(i);

			yield break;
		}

		public static IEnumerable<T> repeat<T>(this T v, int count)
		{
			for (int i = 0; i < count; i++)
				yield return v;

			yield break;
		}

		public static T max<T, V>(this IEnumerable<T> e, Func<T, V> transformFn)
		{
			var mo = MathOperations.Get<V>();

			V maxv = mo.min;
			T r = default(T);

			foreach (var i in e) {
				V v = transformFn(i);
				if (mo.gt(v, maxv)) {
					maxv = v;
					r = i;
				}
			}

			return r;
		}

		public static T min<T, V>(this IEnumerable<T> e, Func<T, V> transformFn)
		{
			var mo = MathOperations.Get<V>();

			V minv = mo.max;
			T r = default(T);

			foreach (var i in e) {
				V cv = transformFn(i);
				if (mo.lt(cv, minv)) {
					minv = cv;
					r = i;
				}
			}

			return r;
		}

		public static IEnumerable<T[]> Tuples<T>(this IEnumerable<T> e, int count)
		{
			T[] array = e.ToArray<T>();
			T[] result = new T[count];

			foreach (T[] r in array.Tuples(count, result, 0, 0))
			{
				yield return r;
			}

			yield break;
		}

		private static IEnumerable<T[]> Tuples<T>(this T[] array, int count, T[] result, int startIndex, int resultIndex)
		{
			for (int i = startIndex; i < array.Length - count + 1; i++)
			{
				result[resultIndex] = array[i];
				if (count > 1)
				{
					foreach (T[] r in array.Tuples(count - 1, result, i + 1, resultIndex + 1))
					{
						yield return r;
					}
				}
				else
				{
					yield return result;
				}
			}

			yield break;
		}
	}
}

