using System;
using System.Collections;
using System.Collections.Generic;

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
	}
}

