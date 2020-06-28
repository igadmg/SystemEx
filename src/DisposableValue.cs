using System;

namespace SystemEx
{
	public class DisposableValue : IDisposable
	{
		IDisposable value;

		public DisposableValue() { }
		public DisposableValue(IDisposable initialValue) { value = initialValue; }

		public IDisposable _ { get => Value; set => Value = value; }
		public IDisposable Value {
			get => value;
			set {
				this.value?.Dispose();
				this.value = value;
			}
		}

		public void Dispose()
		{
			value?.Dispose();
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
				this.value?.Dispose();
				this.value = value;
			}
		}

		public void Dispose()
		{
			value?.Dispose();
		}
	}
}
