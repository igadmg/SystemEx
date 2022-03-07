using System;
using System.Collections.Generic;

namespace SystemEx
{
	public static class DictionaryEx
	{
		public static string ToString(this IDictionary<string, string> d, char separator)
		{
			List<string> l = new List<string>(d.Count * 2);
			foreach (var p in d)
			{
				l.Add(p.Key);
				l.Add(p.Value);
			}

			return l.Join(separator);
		}

		public static Tuple<K, V>[] ToArray<K, V>(this IDictionary<K, V> d)
		{
			Tuple<K, V>[] r = new Tuple<K, V>[d.Count];
			int i = 0;
			foreach (var kvp in d)
			{
				r[i++] = Tuple.Create(kvp.Key, kvp.Value);
			}

			return r;
		}

		public static R[] ToArray<R, K, V>(this IDictionary<K, V> d, Func<Tuple<K, V>, R> t)
		{
			R[] r = new R[d.Count];
			int i = 0;
			foreach (var kvp in d)
			{
				r[i++] = t(Tuple.Create(kvp.Key, kvp.Value));
			}

			return r;
		}

		public static IDictionary<K, V> Add<K, V>(this IDictionary<K, V> d, Tuple<K, V> item)
		{
			d.Add(item.Item1, item.Item2);
			return d;
		}

		public static IDictionary<K, V> Insert<K, V>(this IDictionary<K, V> d, params Tuple<K, V>[] items)
		{
			foreach (var item in items)
			{
				d.Add(item);
			}

			return d;
		}

		public static IDictionary<K, V> Insert<K, V, I>(this IDictionary<K, V> d, Func<I, Tuple<K, V>> t, params I[] items)
		{
			foreach (var item in items)
			{
				d.Add(t(item));
			}

			return d;
		}

		public static int Accumulate<K>(this Dictionary<K, int> d, K key)
		{
			if (!d.TryGetValue(key, out int i))
				d.Add(key, ++i);
			else
				d[key] = ++i;

			return i;
		}

		public static V Get<K, V>(this Dictionary<K, V> d, K key, V dv = default)
		{
			V v;
			if (key != null && d.TryGetValue(key, out v))
				return v;

			return dv;
		}

		public static V Get<K, V>(this Dictionary<K, V> d, K key, Func<V> fdv)
		{
			V v;
			if (key != null && d.TryGetValue(key, out v))
				return v;

			return fdv();
		}

		public static V GetOrAdd<K, V>(this IDictionary<K, V> d, K key, Func<V> ctor)
			=> d.GetOrAdd(key, ctor != null ? k => ctor() : (Func<K, V>)null);

		public static V GetOrAdd<K, V>(this IDictionary<K, V> d, K key, Func<K, V> ctor)
		{
			V v;
			if (!d.TryGetValue(key, out v))
			{
				if (ctor != null)
				{
					v = ctor(key);
					d.Add(key, v);
				}
				else
					v = default;
			}

			return v;
		}

		public static int GetId<K>(this IDictionary<K, int> d, K key)
		{
			int id;
			if (!d.TryGetValue(key, out id))
			{
				id = d.Count;
				d.Add(key, id);
			}

			return id;
		}

		public static bool Remove<K, V>(this IDictionary<K, V> d, K key, Action<V> executeFn)
		{
			if (d.TryGetValue(key, out V v))
			{
				executeFn(v);
				d.Remove(key);
				return true;
			}

			return false;
		}

		public static bool Remove<K, V>(this IDictionary<K, V> d, object o, Action<V> executeFn)
		{
			if (o is K key)
			{
				return d.Remove(key, executeFn);
			}

			return false;
		}

		public static void Clear<K, V>(this IDictionary<K, V> d, Action<K, V> executeFn)
		{
			foreach (var p in d)
				executeFn(p.Key, p.Value);

			d.Clear();
		}

#if NET20_OR_GREATER
		public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> tuple, out T1 key, out T2 value)
		{
			key = tuple.Key;
			value = tuple.Value;
		}
#endif
	}
}
