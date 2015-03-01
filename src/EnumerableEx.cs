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

		public static T max<T>(this IEnumerable<T> source, Func<T, float> selector)
		{
			float maxv = float.MinValue;
			T r = default(T);
			foreach (var i in source) {
				float v = selector(i);
				if (v > maxv) {
					maxv = v;
					r = i;
				}
			}
			return r;
		}
	}
}

