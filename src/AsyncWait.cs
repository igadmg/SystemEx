using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEx
{
	public abstract class AsyncWait
	{
		public object AsyncState
		{
			get { throw new NotImplementedException(); }
		}

		public System.Threading.WaitHandle AsyncWaitHandle
		{
			get { throw new NotImplementedException(); }
		}

		public bool CompletedSynchronously
		{
			get { throw new NotImplementedException(); }
		}
	}

	public class WaitSkip : AsyncWait, IAsyncResult
	{
		public bool IsCompleted { get { return true; } }
	}

	public class WaitForever : AsyncWait, IAsyncResult
	{
		public bool IsCompleted { get { return false; } }
	}

	public class WaitTrue : AsyncWait, IAsyncResult
	{
		Func<bool> f;

		public WaitTrue(Func<bool> cond) { f = cond; }
		public bool IsCompleted { get { return f(); } }
	}

	public class WaitAny : AsyncWait, IAsyncResult
	{
		IEnumerable<Func<bool>> fs;

		public WaitAny(IEnumerable<Func<bool>> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (f()) return true; return false; } }
	}

	public class WaitEvery : AsyncWait, IAsyncResult
	{
		IEnumerable<Func<bool>> fs;

		public WaitEvery(IEnumerable<Func<bool>> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (!f()) return false; return true; } }
	}
}
