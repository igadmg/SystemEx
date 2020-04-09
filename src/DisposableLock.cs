using System;

namespace SystemEx
{
	public class DisposableLock : IDisposable
	{
		public static DisposableLock Lock(Action fn_)
		{
			return new DisposableLock(fn_);
		}

		public static DisposableLock<T> Lock<T>(T v, Action<T> fn_)
		{
			return new DisposableLock<T>(v, fn_);
		}

		Action disposeFn;

		public DisposableLock(Action disposeFn_)
		{
			disposeFn = disposeFn_;
		}

		public void Dispose()
		{
			disposeFn();
		}
	}

	public class DisposableLock<T> : IDisposable
	{
		T v;
		Action<T> disposeFn;

		public DisposableLock(T v_, Action<T> disposeFn_)
		{
			v = v_;
			disposeFn = disposeFn_;
		}

		public void Dispose()
		{
			disposeFn(v);
		}

		public T Value => v;
		public static implicit operator T(DisposableLock<T> dl) => dl.v;
	}
}
