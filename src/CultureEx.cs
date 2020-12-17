using System;

namespace SystemEx
{
	public static class CultureEx
	{
		public static IDisposable Invariant()
		{
			var currentCulture = System.Globalization.CultureInfo.CurrentCulture;
			System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
			return DisposableLock.Lock(() => {
				System.Globalization.CultureInfo.CurrentCulture = currentCulture;
			});
		}
	}
}
