namespace System
{
#if NET35
	public static class Tuple
	{
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) { return new Tuple<T1, T2>(item1, item2); }
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) { return new Tuple<T1, T2, T3>(item1, item2, item3); }
		public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) { return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4); }
		public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) { return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5); }
		public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) { return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6); }
		public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) { return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7); }
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8) { return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8); }
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9) { return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item1, item2, item3, item4, item5, item6, item7, item8, item9); }
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10) { return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(item1, item2, item3, item4, item5, item6, item7, item8, item9, item10); }
	}

	[Serializable]
	public class Tuple<T1, T2>
	{
		public T1 Item1;
		public T2 Item2;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2)
		{
			Item1 = item1;
			Item2 = item2;
		}

		public override int GetHashCode()
		{
			return Item1.GetHashCode() ^ Item2.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var t = obj as Tuple<T1, T2>;
			return t != null
				? object.Equals(Item1, t.Item1) && object.Equals(Item2, t.Item2)
				: base.Equals(obj);
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1}]", Item1, Item2);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
		}

		public override int GetHashCode()
		{
			return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var t = obj as Tuple<T1, T2, T3>;
			return t != null
				? object.Equals(Item1, t.Item1) && object.Equals(Item2, t.Item2) && object.Equals(Item3, t.Item3)
				: base.Equals(obj);
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2}]", Item1, Item2, Item3);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3, T4>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;
		public T4 Item4;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
		}

		public override int GetHashCode()
		{
			return Item1.GetHashCode() ^ Item2.GetHashCode() ^ Item3.GetHashCode() ^ Item4.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var t = obj as Tuple<T1, T2, T3, T4>;
			return t != null
				? object.Equals(Item1, t.Item1) && object.Equals(Item2, t.Item2) && object.Equals(Item3, t.Item3) && object.Equals(Item4, t.Item4)
				: base.Equals(obj);
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2},{3}]", Item1, Item2, Item3, Item4);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;
		public T4 Item4;
		public T5 Item5;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2},{3},{4}]", Item1, Item2, Item3, Item4, Item5);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;
		public T4 Item4;
		public T5 Item5;
		public T6 Item6;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2},{3},{4},{5}]", Item1, Item2, Item3, Item4, Item5, Item6);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;
		public T4 Item4;
		public T5 Item5;
		public T6 Item6;
		public T7 Item7;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2},{3},{4},{5},{6}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;
		public T4 Item4;
		public T5 Item5;
		public T6 Item6;
		public T7 Item7;
		public T8 Item8;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;
			Item8 = item8;
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2},{3},{4},{5},{6},{7}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;
		public T4 Item4;
		public T5 Item5;
		public T6 Item6;
		public T7 Item7;
		public T8 Item8;
		public T9 Item9;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;
			Item8 = item8;
			Item9 = item9;
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2},{3},{4},{5},{6},{7},{8}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9);
		}
	}

	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
	{
		public T1 Item1;
		public T2 Item2;
		public T3 Item3;
		public T4 Item4;
		public T5 Item5;
		public T6 Item6;
		public T7 Item7;
		public T8 Item8;
		public T9 Item9;
		public T10 Item10;

		public Tuple() { }
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
		{
			Item1 = item1;
			Item2 = item2;
			Item3 = item3;
			Item4 = item4;
			Item5 = item5;
			Item6 = item6;
			Item7 = item7;
			Item8 = item8;
			Item9 = item9;
			Item10 = item10;
		}

		public override string ToString()
		{
			return string.Format("[Tuple][{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10);
		}
	}
#endif
}
