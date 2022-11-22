using System;

namespace SystemEx
{
	/*
	public class GuardValue<T> : IDisposable
	{
		public static GuardValue<T> Guard(ref T v, T newv)
		{
			return DisposableLock.Lock(v, _ => v = _).Also(_ => v = newv);
		}

		public void Dispose()
		{
		}
	}
	*/

	public interface IDisposable<T> : IDisposable
	{
		T Value { get; }
	}

	public class DisposableProxy<T> : IDisposable<T>
	{
		public T Value { get; private set; }
		protected IDisposable dsp;

		public DisposableProxy(IDisposable disposable, T value)
		{
			Value = value;
			dsp = disposable;
		}

		public void Dispose()
		{
			dsp.Dispose();
		}
	}

	public static class DisposableLockEx
	{
		public static DisposableLock<T> Lock<T>(this T v, Action<T> dfn)
			=> DisposableLock.Lock(v, dfn);

		public static DisposableLock<T> Lock<T>(this T v, Action<T> vfn, Action<T> dfn)
			=> DisposableLock.Lock(v.Also(_ => vfn(v)), dfn);

		public static DisposableProxy<T> Wrap<T>(this IDisposable disposable, T value)
			=> new DisposableProxy<T>(disposable, value);
	}

	public class DisposableLock : IDisposable
	{
		public static DisposableLock empty = new DisposableLock(() => { });

		public static DisposableLock Lock(Action fn)
		{
			return new DisposableLock(fn);
		}

		public static DisposableLock<T> Lock<T>(T v, Action<T> fn)
		{
			return new DisposableLock<T>(v, fn);
		}

		public static DisposableLock<T> Lock<T>(Func<T> v, Action<T> fn)
		{
			return new DisposableLock<T>(v(), fn);
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

	public class DisposableLock<T> : IDisposable<T>, IDisposable
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
		public T _ => v;
		public static implicit operator T(DisposableLock<T> dl) => dl.v;
	}
}
