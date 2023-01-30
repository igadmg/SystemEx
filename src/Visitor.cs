using System;

namespace SystemEx
{
	class Acceptor<T> : IAccept<T>
	{
		private readonly T v;

		public Acceptor(T v)
		{
			this.v = v;
		}

		public void Accept(IVisit<T> visitor)
		{
			visitor.Visit(v);
		}

		public void Accept(Action<T> visitor)
		{
			visitor(v);
		}
	}


	public interface IAccept<T>
	{
		public static IAccept<T> Accept(T v) => new Acceptor<T>(v);

		void Accept(IVisit<T> visitor);
		void Accept(Action<T> visitor);
	}

	public interface IVisit<T>
	{
		void Visit(T item);
	}

	public static class VisitEx
	{
		/*
		public static void Accept<T>(this IEnumerable<T> e, IVisit<T> visitor)
			where T : IAccept<T>
		{
			foreach (var i in e)
			{
				i.Accept(visitor);
			}
		}
		*/
	}
}
