using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemEx
{
	public class LazyNotNull<T>
	{
		Lazy<T> l;
		Func<Lazy<T>> clfn;

		public LazyNotNull() { clfn = () => l = new Lazy<T>(); l = clfn(); }
		public LazyNotNull(bool isThreadSafe) { clfn = () => l = new Lazy<T>(isThreadSafe); }
		public LazyNotNull(Func<T> valueFactory) { clfn = () => l = new Lazy<T>(valueFactory); l = clfn(); }
		public LazyNotNull(LazyThreadSafetyMode mode) { clfn = () => l = new Lazy<T>(mode); l = clfn(); }
		public LazyNotNull(Func<T> valueFactory, bool isThreadSafe) { clfn = () => l = new Lazy<T>(valueFactory, isThreadSafe); l = clfn(); }
		public LazyNotNull(Func<T> valueFactory, LazyThreadSafetyMode mode) { clfn = () => l = new Lazy<T>(valueFactory, mode); l = clfn(); }

		public bool IsValueCreated => l.IsValueCreated;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value {
			get {
				T tv = l.Value;
				if (tv == null)
				{
					l = clfn();
				}
				return tv;
			}
		}

		public override string ToString() => l.ToString();
	}
}
