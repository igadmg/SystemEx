using System;
using System.Collections.Generic;

namespace SystemEx
{
	public static class DictionaryEx
	{
		public static Tuple<K, V>[] ToArray<K, V>(this IDictionary<K, V> d)
		{
			Tuple<K, V>[] r = new Tuple<K, V>[d.Count];
			int i = 0;
			foreach (var kvp in d) {
				r[i++] = Tuple.Create(kvp.Key, kvp.Value);
			}

			return r;
		}

		public static R[] ToArray<R, K, V>(this IDictionary<K, V> d, Func<Tuple<K, V>, R> t)
		{
			R[] r = new R[d.Count];
			int i = 0;
			foreach (var kvp in d) {
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
			foreach (var item in items) {
				d.Add(item);
			}

			return d;
		}

		public static IDictionary<K, V> Insert<K, V, I>(this IDictionary<K, V> d, Func<I, Tuple<K, V>> t, params I[] items)
		{
			foreach (var item in items) {
				d.Add(t(item));
			}

			return d;
		}

		public static V Get<K, V>(this Dictionary<K, V> d, K key)
		{
			V v;
			if (d.TryGetValue(key, out v))
				return v;

			return default(V);
		}

		public static V GetOrAdd<K, V>(this Dictionary<K, V> d, K key, Func<K, V> ctor)
		{
			V v;
			if (!d.TryGetValue(key, out v))
			{
				v = ctor(key);
				d.Add(key, v);
			}

			return v;
		}

		public static int GetId<K>(this Dictionary<K, int> d, K key)
		{
			int id;
			if (!d.TryGetValue(key, out id)) {
				id = d.Count;
				d.Add(key, id);
			}

			return id;
		}
	}
}
