using System;

namespace SystemEx
{
	public class DisposableEvent<T> : IDisposable
	{
		T o;
		Action<T> subFn;
		Action<T> unsubFn;

		public DisposableEvent(Action<T> subFn, Action<T> unsubFn)
		{
			this.subFn = subFn;
			this.unsubFn = unsubFn;
		}

		public T _ {
			set {
				if (o != null) unsubFn?.Invoke(o);
				o = value;
				if (o != null) subFn?.Invoke(o);
			}
		}

		public void Dispose()
		{
			if (o != null) unsubFn?.Invoke(o);
		}
	}
}
