using System;

namespace SystemEx
{
	public static class StopwatchScope
	{
		public static IDisposable Measure(Action<TimeSpan> result)
		{
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			return DisposableLock.Lock(() => {
				sw.Stop();
				result(sw.Elapsed);
			});
		}
	}
}
