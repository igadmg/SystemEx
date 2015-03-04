using System;
using System.Collections.Generic;

namespace SystemEx
{
	[Serializable]
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

	[Serializable]
	public class WaitSkip : AsyncWait, IAsyncResult
	{
		public bool IsCompleted { get { return true; } }
	}

	[Serializable]
	public class WaitForever : AsyncWait, IAsyncResult
	{
		public bool IsCompleted { get { return false; } }
	}

	[Serializable]
	public class WaitTrue : AsyncWait, IAsyncResult
	{
		Func<bool> f;

		public WaitTrue(Func<bool> cond) { f = cond; }
		public bool IsCompleted { get { return f(); } }
	}

	[Serializable]
	public class WaitAny : AsyncWait, IAsyncResult
	{
		IEnumerable<Func<bool>> fs;

		public WaitAny(IEnumerable<Func<bool>> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (f()) return true; return false; } }
	}

	[Serializable]
	public class WaitEvery : AsyncWait, IAsyncResult
	{
		IEnumerable<Func<bool>> fs;

		public WaitEvery(IEnumerable<Func<bool>> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (!f()) return false; return true; } }
	}
}
