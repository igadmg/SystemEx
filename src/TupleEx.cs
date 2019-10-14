using System;

namespace SystemEx
{
	public static class TupleEx
	{
		public static Tuple<T1, T1> Sort<T1>(this Tuple<T1, T1> t) where T1 : IComparable
		{
			if (t.Item1.CompareTo(t.Item2) > 0)
				return Tuple.Create(t.Item2, t.Item1);
			return t;
		}
	}
}
