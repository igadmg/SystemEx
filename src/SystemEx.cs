using System;
using System.Collections;

namespace SystemEx
{
	public static class SystemEx
	{
		public static Lazy<T> lazy<T>(this T v) => new Lazy<T>(() => v);
		public static Lazy<T> lazy<T>(this Func<T> fn) => new Lazy<T>(() => fn());

		public static Action<T> empty<T>() where T : IEnumerable => (t => { });

		public static DisposableLock dlock(this Action fn) => DisposableLock.Lock(fn);
		public static DisposableLock<T> dlock<T>(this T v, Action<T> fn) => DisposableLock.Lock(v, fn);

		public static WeakReference weak(this object v) => new WeakReference(v);
		public static WeakReference<T> weak<T>(this T v) where T : class => new WeakReference<T>(v);
	}
}
