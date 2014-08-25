using System;

namespace SystemEx
{
	public static class Watch
	{
		public static IClockProvider clock = new SystemClockProvider();

		public static Stopwatch Stopwatch(float dt)
		{
			var sw = new Stopwatch(dt, true).Reset();
			return sw;
		}

		public static IAsyncResult WaitStopwatch(float dt)
		{
			var sw = new Stopwatch(dt, false).Reset();
			return new WaitTrue(() => (bool)sw);
		}
	}

	public interface IClockProvider
	{
		float tick { get; }
	}

	public class SystemClockProvider : IClockProvider
	{
		public float tick { get { return getTick(); } }



		long base_;
		public SystemClockProvider()
		{
			base_ = System.Diagnostics.Stopwatch.GetTimestamp();
		}

		float getTick()
		{
			var ts = System.Diagnostics.Stopwatch.GetTimestamp();
			return (ts - base_) / ((float)System.Diagnostics.Stopwatch.Frequency);
		}
	}


	public class Stopwatch
	{
		public static float clock { get { return Watch.clock.tick; } }

		private float dt_;
		private float t_;
		private bool autoreset_;

		public Stopwatch(float dt, bool autoreset)
		{
			dt_ = dt;
			autoreset_ = autoreset;
			Reset();
		}

		public Stopwatch Reset()
		{
			t_ = clock;
			return this;
		}

		public static implicit operator bool(Stopwatch sw)
		{
			var r = (clock - sw.t_) > sw.dt_;
			if (sw.autoreset_ && r)
				sw.Reset();
			return r;
		}
	}
}