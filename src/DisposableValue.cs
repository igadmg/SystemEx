using System;

namespace SystemEx
{
	public static class DisposableValueEx
	{
	}

	// Used to store disposable objects in a filed.
	// When new value is assigned to DisposableValue<T> field
	// previous value is disposed.
	public class DisposableValue : IDisposable
	{
		bool isDisposed = false;
		IDisposable value;

		public DisposableValue() { }
		public DisposableValue(IDisposable initialValue) { value = initialValue; }

		public IDisposable _ { get => Value; set => Value = value; }
		public IDisposable Value {
			get => value;
			set {
				Dispose();
				this.value = value;
				isDisposed = false;
			}
		}

		public void Dispose()
		{
			if (!isDisposed)
			{
				value?.Dispose();
				value = null;
				isDisposed = true;
			}
		}
	}

	public class DisposableValue<T> : IDisposable
		where T : IDisposable
	{
		T value;

		public DisposableValue() { }
		public DisposableValue(T initialValue) { value = initialValue; }

		public T _ { get => Value; set => Value = value; }
		public T Value {
			get => value;
			set {
				Dispose();
				this.value = value;
			}
		}

		public void Dispose()
		{
			if (value != null)
			{
				value?.Dispose();
				value = default;
			}
		}
	}
}
