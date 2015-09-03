using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemEx
{
	public delegate int Comparison<T, U>(T x, U y);

	public class LambdaComparer<T> : IComparer<T>, IEqualityComparer<T>, IComparer, IEqualityComparer
	{
		private Comparison<T> c;

		public LambdaComparer(Comparison<T> comparison)
		{
			c = comparison;
		}

		public int Compare(T x, T y)
		{
			return c(x, y);
		}

		public int Compare(object x, object y)
		{
			return c((T)x, (T)y);
		}

		public bool Equals(T x, T y)
		{
			return Compare(x, y) == 0;
		}

		public new bool Equals(object x, object y)
		{
			return Compare(x, y) == 0;
		}

		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}

		public int GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}
	}

	public class LambdaComparer<T, U> : IComparer, IEqualityComparer
	{
		private Comparison<T, U> c;

		public LambdaComparer(Comparison<T, U> comparison)
		{
			c = comparison;
		}

		public int Compare(object x, object y)
		{
			return c((T)x, (U)y);
		}

		public new bool Equals(object x, object y)
		{
			return Compare(x, y) == 0;
		}

		public int GetHashCode(object obj)
		{
			return obj.GetHashCode();
		}
	}

	public static class LambdaComparer
	{
		static public LambdaComparer<T> Create<T>(Comparison<T> comparison)
		{
			return new LambdaComparer<T>(comparison);
		}

		static public IComparer Create2<T, U>(Comparison<T, U> comparison)
		{
			return new LambdaComparer<T, U>(comparison);
		}
	}
}
