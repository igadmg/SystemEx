using System;
using System.Reflection;

namespace SystemEx
{
	public static class ObjectEx
	{
		// Kotlin: fun <T, R> T.let(block: (T) -> R): R
		public static R Let<T, R>(this T self, Func<T, R> block)
		{
			return block(self);
		}

		// Kotlin: fun <T> T.also(block: (T) -> Unit): T
		public static T Also<T>(this T self, Action<T> block)
		{
			block(self);
			return self;
		}

		public static T IfValid<T>(this T self, Action<T> block)
		{
			if (self != null) block(self);
			return self;
		}

		public static T UnlessValid<T>(this T self, Action<T> block)
		{
			if (self == null) block(self);
			return self;
		}

		public static T Or<T>(this T self, Func<T> block)
		{
			return (self == null) ? block() : self;
		}

		public static R Elvis<T, R>(this T self, Func<T, R> block, R defualtValue = default)
		{
			return (self != null) ? block(self) : defualtValue;
		}

		public static T GetPropertyValue<T>(this object o, string name)
		{
			return (T)o.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(o, null);
		}
	}
}
