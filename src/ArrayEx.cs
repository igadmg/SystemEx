using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SystemEx
{
	public static class ArrayEx
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T at<T>(this T[] a, int index)
		{
			if (index < 0)
				return a[a.Length + index];
			return a[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T at<T>(this IList<T> a, int index)
		{
			if (index < 0)
				return a[a.Count + index];
			return a[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T at<T>(this T[] a, uint index)
		{
			return a[index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T at<T>(this IList<T> a, uint index)
		{
			return a[(int)index];
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T[] at<T>(this T[] a, int index, T value)
		{
			if (index < 0)
				a[a.Length + index] = value;
			else
				a[index] = value;

			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void at<T>(this IList<T> a, int index, T value)
		{
			if (index < 0)
				a[a.Count + index] = value;
			else
				a[index] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T[] at<T>(this T[] a, uint index, T value)
		{
			a[index] = value;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void at<T>(this IList<T> a, uint index, T value)
		{
			a[(int)index] = value;
		}

		public static T any<T>(this T[] a)
		{
			return a[RandomEx.instance.Next(a.Length)];
		}

		public static T any<T>(this IList<T> a)
		{
			return a[RandomEx.instance.Next(a.Count)];
		}

		public static bool IsEmptyOrNull<T>(this T[] a)
			=> (a?.Length ?? 0) == 0;

		public static bool IsEmptyOrNull<T>(this IList<T> a)
			=> (a?.Count ?? 0) == 0;

		//public static IList<T> Shuffle<T>(this IList<T> list)
		//	=> list.Shuffle(RandomEx.instance);

		public delegate void SliceByFn<T>(Span<T> span);
		public static void SliceBy<T>(this T[] a, int sliceSize, SliceByFn<T> action)
		{
			for (int i = 0; i < a.Length; i += sliceSize)
			{
				action(new Span<T>(a, i, sliceSize));
			}
		}

		public static IEnumerable<T[]> ChunkBy<T>(this T[] a, int chunkSize)
		{
			for (int i = 0; i < a.Length; i += chunkSize)
			{
				var buffer = new T[chunkSize];
				Array.Copy(a, i, buffer, 0, Math.Min(chunkSize, a.Length - i));
				yield return buffer;
			}
		}

		public static IList<T> Shuffle<T>(this IList<T> list, IRandomGenerator<int> rgi)
		{
			int n = list.Count;
			while (n > 1)
			{
				int k = rgi.Next(max: n--);
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

		public static T[] Fill<T>(this T[] array, T value)
		{
			for (int i = 0; i < array.Length; i++)
				array[i] = value;
			//Array.Fill(array, value);
			return array;
		}

		public static bool Contains<T>(this T[] array, T value)
			=> Array.IndexOf(array, value) != -1;

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

		public static T[] Modify<T>(this T[] array, Func<T, T> fn)
		{
			for (int i = 0; i < array.Length; i++)
				array[i] = fn(array[i]);

			return array;
		}

		public static U[] Transform<T, U>(this T[] array, Func<T, U> fn)
		{
			U[] result = new U[array.Length];

			for (int i = 0; i < array.Length; i++)
				result[i] = fn(array[i]);

			return result;
		}

		public static T[] Sort<T>(this T[] array)
		{
			Array.Sort(array);
			return array;
		}

		public static T[] Sort<T>(this T[] array, Comparison<T> c)
		{
			Array.Sort(array, c);
			return array;
		}

		public static T[] Concat<T>(this T[] array, T value)
		{
			if (array == null)
				return new T[] { value };

			T[] result = new T[array.Length + 1];

			Array.Copy(array, 0, result, 0, array.Length);
			result[result.Length - 1] = value;

			return result;
		}

		public static T[] Concat<T>(this T value, T[] array)
		{
			if (array == null)
				return new T[] { value };

			T[] result = new T[array.Length + 1];

			result[0] = value;
			Array.Copy(array, 0, result, 1, array.Length);

			return result;
		}

		public static T[] Skip<T>(this T[] array, int count)
		{
			if (count < array.Length)
			{
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

			for (int i = 0; i < result.Length; i++)
			{
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
			for (int i = 0; i < array.Length; i++)
			{
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
