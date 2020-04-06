using System;
using System.Collections;
using System.Collections.Generic;

namespace SystemEx
{
	public class LazyFuncEnumerator<T> : IEnumerator<T>
	{
		Func<T> func;
		Lazy<T> current = new Lazy<T>();
		public T Current => current.Value;
		object IEnumerator.Current => Current;

		public LazyFuncEnumerator(Func<T> fn_)
		{
			func = fn_;
		}
		public void Dispose() { }

		public bool MoveNext()
		{
			Reset();
			return true;
		}

		public void Reset()
		{
			if (current.IsValueCreated)
				current = new Lazy<T>(() => func());
		}
	}
}
