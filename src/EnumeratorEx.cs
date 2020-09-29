using System.Collections;
using System.Collections.Generic;

namespace SystemEx
{
	public static class EnumeratorEx
	{
		public static IEnumerable<T> Enumerate<T>(this IEnumerator enumerator)
		{
			while (enumerator.MoveNext())
			{
				yield return (T)enumerator.Current;
			}
		}
	}
}
