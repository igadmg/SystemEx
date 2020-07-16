using System;

namespace SystemEx
{
	public interface ICloneable<T> : ICloneable
	{
		new T Clone();
	}
}
