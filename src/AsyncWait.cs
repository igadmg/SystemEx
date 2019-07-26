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



		public static IAsyncResult Skip() { return new WaitSkip(); }
		public static IAsyncResult Forever() { return new WaitForever(); }
		public static IAsyncResult True(Func<bool> cond) { return new WaitTrue(cond); }
		public static IAsyncResult Any(IEnumerable<Func<bool>> cond) { return new WaitAny(cond); }
		public static IAsyncResult Any(IEnumerable<IAsyncResult> cond) { return new WaitAnyAsync(cond); }
		public static IAsyncResult Every(IEnumerable<Func<bool>> cond) { return new WaitEvery(cond); }
		public static IAsyncResult Every(IEnumerable<IAsyncResult> cond) { return new WaitEveryAsync(cond); }
	}

	[Serializable]
	internal class WaitSkip : AsyncWait, IAsyncResult
	{
		public bool IsCompleted { get { return true; } }
	}

	[Serializable]
	internal class WaitForever : AsyncWait, IAsyncResult
	{
		public bool IsCompleted { get { return false; } }
	}

	[Serializable]
	internal class WaitTrue : AsyncWait, IAsyncResult
	{
		Func<bool> f;

		public WaitTrue(Func<bool> cond) { f = cond; }
		public bool IsCompleted { get { return f(); } }
	}

	[Serializable]
	internal class WaitAny : AsyncWait, IAsyncResult
	{
		IEnumerable<Func<bool>> fs;

		public WaitAny(IEnumerable<Func<bool>> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (f()) return true; return false; } }
	}

	[Serializable]
	internal class WaitAnyAsync : AsyncWait, IAsyncResult
	{
		IEnumerable<IAsyncResult> fs;

		public WaitAnyAsync(IEnumerable<IAsyncResult> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (f.IsCompleted) return true; return false; } }
	}

	[Serializable]
	internal class WaitEvery : AsyncWait, IAsyncResult
	{
		IEnumerable<Func<bool>> fs;

		public WaitEvery(IEnumerable<Func<bool>> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (!f()) return false; return true; } }
	}

	[Serializable]
	internal class WaitEveryAsync : AsyncWait, IAsyncResult
	{
		IEnumerable<IAsyncResult> fs;

		public WaitEveryAsync(IEnumerable<IAsyncResult> cond) { fs = cond; }
		public bool IsCompleted { get { foreach (var f in fs) if (!f.IsCompleted) return false; return true; } }
	}
}
