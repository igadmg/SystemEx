using System;

namespace SystemEx
{
	public static class SystemEx
	{
		public static Lazy<T> lazy<T>(Func<T> fn) => new Lazy<T>(() => fn());

		public static DisposableLock dlock(Action fn) => DisposableLock.Lock(fn);
		public static DisposableLock<T> dlock<T>(T v, Action<T> fn) => DisposableLock.Lock(v, fn);
	}
}
