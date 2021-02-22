using System.Collections.Generic;

namespace SystemEx
{
	public interface IAccept<TVisitor>
	{
		void Accept(TVisitor visitor);
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
