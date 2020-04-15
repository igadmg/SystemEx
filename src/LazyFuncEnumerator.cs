using System;
using System.Collections;
using System.Collections.Generic;

namespace SystemEx
{
	public class LazyFuncEnumerator<T> : IEnumerator<T>
	{
		Func<T> func;
		Lazy<T> current;
		public T Current => current.Value;
		object IEnumerator.Current => Current;

		public LazyFuncEnumerator(Func<T> fn_)
		{
			func = fn_;
			Reset();
		}
		public void Dispose() { }

		public bool MoveNext()
		{
			if (current.IsValueCreated)
				Reset();
			return true;
		}

		public void Reset()
		{
			current = new Lazy<T>(() => func());
		}
	}
}
