using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SystemEx
{
	public static class ArrayEx
	{
		public static T at<T>(this T[] a, int index)
		{
			if (index < 0)
				return a[a.Length + index];
			return a[index];
		}

		public static T at<T>(this IList<T> a, int index)
		{
			if (index < 0)
				return a[a.Count + index];
			return a[index];
		}

		public static void at<T>(this T[] a, int index, T value)
		{
			if (index < 0)
				a[a.Length + index] = value;
			else
				a[index] = value;
		}

		public static void at<T>(this IList<T> a, int index, T value)
		{
			if (index < 0)
				a[a.Count + index] = value;
			else
				a[index] = value;
		}

        public static T any<T>(this T[] a)
        {
            return a[new Random().Next(a.Length)];
        }

        public static T any<T>(this IList<T> a)
        {
            return a[new Random().Next(a.Count)];
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                list.Swap(n, k);
            }

            return list;
        }

        public static IList<T> Swap<T>(this IList<T> l, int a, int b)
		{
			T tmp = l[a];
			l[a] = l[b];
			l[b] = tmp;
			return l;
		}

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

		public static U[] Transform<T, U>(this T[] array, Func<T, U> fn)
		{
			U[] result = new U[array.Length];

			for (int i = 0; i < array.Length; i++)
				result[i] = fn(array[i]);

			return result;
		}

		public static T[] Sort<T>(this T[] array, Comparison<T> c)
		{
			Array.Sort(array, c);
			return array;
		}

		public static T[] Concat<T>(this T[] array, T value)
		{
			T[] result = new T[array.Length + 1];

			Array.Copy(array, 0, result, 0, array.Length);
			result[result.Length - 1] = value;

			return result;
		}

		public static T[] Concat<T>(this T value, T[] array)
		{
			T[] result = new T[array.Length + 1];

			result[0] = value;
			Array.Copy(array, 0, result, 1, array.Length);

			return result;
		}

		public static T[] Skip<T>(this T[] array, int count)
		{
			if (count < array.Length) {
				T[] result = new T[array.Length - count];
				Array.Copy(array, count, result, 0, result.Length);
				return result;
			}
			return new T[] { };
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

		public static int CalcHashCode<T>(this T[] array)
		{
			int result = 0;
			for (int i = 0; i < array.Length; i++) {
				result = (result * 397) ^ array[i].GetHashCode();
			}
			return result;
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