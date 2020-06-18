using System;

namespace SystemEx
{
	public static class RandomEx
	{
		public static Random instance = new Random();
	}

	public interface IRandomGenerator
	{
		IRandomGenerator<T> Cast<T>();
	}

	public interface IRandomGenerator<T> : IRandomGenerator
	{
		T Next(T min = default, T max = default);
	}
}
