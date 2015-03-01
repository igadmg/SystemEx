using System;


namespace SystemEx
{
	public class _
	{
		public static ActionContainer a<T1>(Action<T1> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1)); }
		public static ActionContainer a<T1, T2>(Action<T1, T2> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2)); }
		public static ActionContainer a<T1, T2, T3>(Action<T1, T2, T3> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2), typeof(T3)); }
		public static ActionContainer a<T1, T2, T3, T4>(Action<T1, T2, T3, T4> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2), typeof(T3), typeof(T4)); }
		public static ActionContainer a<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)); }
		public static ActionContainer a<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)); }
		public static ActionContainer a<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)); }
		public static ActionContainer a<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8)); }
		public static ActionContainer a<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> lambda) { return new ActionContainer((Delegate)lambda, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9)); }

		public static FuncContainer f<T1, R>(Func<T1, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1)); }
		public static FuncContainer f<T1, T2, R>(Func<T1, T2, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2)); }
		public static FuncContainer f<T1, T2, T3, R>(Func<T1, T2, T3, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2), typeof(T3)); }
		public static FuncContainer f<T1, T2, T3, T4, R>(Func<T1, T2, T3, T4, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2), typeof(T3), typeof(T4)); }
		public static FuncContainer f<T1, T2, T3, T4, T5, R>(Func<T1, T2, T3, T4, T5, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)); }
		public static FuncContainer f<T1, T2, T3, T4, T5, T6, R>(Func<T1, T2, T3, T4, T5, T6, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)); }
		public static FuncContainer f<T1, T2, T3, T4, T5, T6, T7, R>(Func<T1, T2, T3, T4, T5, T6, T7, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)); }
		public static FuncContainer f<T1, T2, T3, T4, T5, T6, T7, T8, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8)); }
		public static FuncContainer f<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> lambda) { return new FuncContainer((Delegate)lambda, typeof(R), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9)); }

		public static T if_<T>(bool b, Func<T> yes, Func<T> no)
		{
			if (b) return yes();
			else return no();
		}
	}
}
