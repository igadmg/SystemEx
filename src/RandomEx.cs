using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemEx
{
	public static class RandomEx
	{
		public static System.Random instance = new System.Random();

		public static IEnumerable<T> NextN<T>(this IRandomGenerator<int> irg, T[] array, int count)
		{
			HashSet<int> visited = new HashSet<int>();

			while (count > 0)
			{
				int i = irg.Next(max: array.Length);
				if (visited.Contains(i))
					continue;

				visited.Add(i);
				yield return array[i];
				count--;
			}
		}

		#region float

		public static float Sign(this IRandomGenerator<float> frg)
			=> frg.Next(-0.5f, 0.5f) < 0 ? -1.0f : 1.0f;

		public static float Next01(this IRandomGenerator<float> frg)
			=> frg.Next(0f, 1.0f + float.Epsilon);

		#endregion float

		public static (float frequency, T item)[] CalculateItemFrequencies<T>(this FrequencyOf<T>[] items)
		{
			float total = 0;
			return items
				.Select(item => { total += item.frequency; return (total, item.item); })
				.ToArray();
		}

		public static T NextOf<T>(this IRandomGenerator<float> frg, (float frequency, T item)[] itemFrequencies)
		{
			float s = frg.Next(itemFrequencies.Last().frequency);
			return itemFrequencies.Where(f => s <= f.frequency).First().item;
		}

		public static List<T> NextNOf<T>(this IRandomGenerator rg, int count, FrequencyOf<T>[] items)
		{
			List<T> result = new List<T>(count);

			int ii = 0;
			foreach (var item in items)
			{
				int ei = (int)(item.frequency / 100.0f * count);
				for (int i = 0; i < ei; i++, ii++)
				{
					result.Add(item.item);
				}
			}

			var frg = rg.Cast<float>();
			var itemFrequencies = items.CalculateItemFrequencies();
			for (; ii < count; ii++)
			{
				result.Add(frg.NextOf(itemFrequencies));
			}

			result.Shuffle(rg.Cast<int>());

			return result;
		}
	}

	public interface IRandomGenerator
	{
		IRandomGenerator<T> Cast<T>();
	}

	public interface IRandomGenerator<T> : IRandomGenerator
	{
		T Next(T min = default, T max = default);
	}

	[Serializable]
	public struct FrequencyOf<T>
	{
		public T item;
		public float frequency; // per hundred
	}
}
