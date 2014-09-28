using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SystemEx
{
	public static class ArrayEx
	{
		public static T[] Initialize<T>(this T[] array, T value)
		{
			for (int i = 0; i < array.Length; i++)
				array[i] = value;
			return array;
		}

		public static bool Contains<T>(this T[] array, T value)
		{
			return Array.IndexOf(array, value) != -1;
		}

		public static bool Compare<T, U>(this T[] array, U[] array2, Func<T, U, bool> cmp)
		{
			if (array.Length != array2.Length)
				return false;

			for (int i = 0; i < array.Length; i++)
				if (!cmp(array[i], array2[i]))
					return false;

			return true;
		}

		public static T[] ForEach<T>(this T[] array, Action<T> action)
		{
			Array.ForEach(array, action);
			return array;
		}

		public static T[] Sort<T>(this T[] array, Comparison<T> c)
		{
			Array.Sort(array, c);
			return array;
		}

		public static T[] Concat<T>(T value, T[] array)
		{
			T[] result = new T[array.Length + 1];

			result[0] = value;
			Array.Copy(array, 0, result, 1, array.Length);

			return result;
		}

		public static T[] Parse<T>(string value)
		{
			string[] tokens = value.Split(new Char[] { ':' });
			T[] result = new T[tokens.Length];

			for (int i = 0; i < result.Length; i++) {
				result[i] = (T)Convert.ChangeType(tokens[i], typeof(T));
			}

			return result;
		}

		public static Dictionary<K, V> ToDictionary<I, K, V>(this Tuple<K, V>[] array)
		{
			Dictionary<K, V> r = new Dictionary<K, V>();
			r.Insert(array);
			return r;
		}

		public static Dictionary<K, V> ToDictionary<I, K, V>(this I[] array, Func<I, Tuple<K, V>> t)
		{
			Dictionary<K, V> r = new Dictionary<K, V>();
			r.Insert(t, array);
			return r;
		}

		public static Stream ToStream(this byte[] array)
		{
			return new MemoryStream(array);
		}

		public static string GetString(this byte[] array)
		{
			return Encoding.UTF8.GetString(array);
		}
	}
}
