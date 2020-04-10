using System;
using System.Collections.Generic;

namespace SystemEx
{
	public class AggregateExceptionScope : IDisposable
	{
		Lazy<List<Exception>> exceptions = new Lazy<List<Exception>>(() => new List<Exception>());

		public void Aggregate(Exception e)
		{
			exceptions.Value.Add(e);
		}

		public void Aggregate(params Exception[] e)
		{
			exceptions.Value.AddRange(e);
		}

		public void Aggregate(IEnumerable<Exception> e)
		{
			exceptions.Value.AddRange(e);
		}

		public void Execute(Action fn)
		{
			try { fn(); }
			catch (Exception e) { exceptions.Value.Add(e); }
		}

		public void Dispose()
		{
			if (exceptions.IsValueCreated)
				throw new AggregateException(exceptions.Value);
		}
	}
}
