using System;

namespace SystemEx
{
	public static class LazyEx
	{
		public static Lazy<T> Lazy<T>(this Lazy<T> l, Func<T> v) => l ?? v.ToLazy();

		public static Lazy<T> ToLazy<T>(this T v)
			=> new Lazy<T>(() => v);

		public static Lazy<T> ToLazy<T>(this Func<T> v)
			=> new Lazy<T>(v);

		public static void Dispose<T>(this Lazy<T> l)
			where T : IDisposable
		{
			if (l.IsValueCreated)
				l.Value?.Dispose();
		}
	}
}
