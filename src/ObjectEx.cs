using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SystemEx
{
	public static class ObjectEx
	{
		// Kotlin: fun <T, R> T.let(block: (T) -> R): R
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static R Let<T, R>(this T self, Func<T, R> block)
		{
			return block(self);
		}

		// Kotlin: fun <T> T.also(block: (T) -> Unit): T
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Also<T>(this T self, Action<T> block)
		{
			block(self);
			return self;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T IfValid<T>(this T self, Action<T> block)
		{
			if (self != null) block(self);
			return self;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T UnlessValid<T>(this T self, Action<T> block)
		{
			if (self == null) block(self);
			return self;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Or<T>(this T self, Func<T> block)
		{
			return (self == null) ? block() : self;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Elvis<T>(this T self, Action<T> block)
		{
			if (self != null) block(self);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static R Elvis<T, R>(this T self, Func<T, R> block, R defualtValue = default)
		{
			return (self != null) ? block(self) : defualtValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Elvis<T>(this T self, Func<T, string> block, string defualtValue = "")
		{
			return (self != null) ? block(self) : defualtValue;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetFieldValue<T>(this object o, string name)
		{
			return (T)o.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(o);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T GetPropertyValue<T>(this object o, string name)
		{
			return (T)o.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(o, null);
		}

		public static void DisposeFields(this object o)
		{
			foreach (var field in o.GetType().GetFields<DisposeAttribute>())
			{
				if (field.FieldType.IsA<IDictionary<object, IDisposable>>())
				{
					var dict = field.GetValue(o) as IEnumerable;
					if (dict == null) continue;

					foreach (object p in dict)
					{
						p.GetFieldValue<IDisposable>("Value")?.Dispose();
					}
				}
				else if (field.FieldType.IsA<IList<IDisposable>>())
				{
					var list = field.GetValue(o) as IEnumerable;
					if (list == null) continue;

					foreach (object p in list)
					{
						(p as IDisposable)?.Dispose();
					}
				}
				else if (field.FieldType.IsA<IDisposable>())
				{
					(field.GetValue(o) as IDisposable).Elvis(d => d.Dispose());
				}
				else
				{
					UnityEngine.Debug.LogWarning($"Don't know how to dispose field: {field.FieldType.Name} {o.GetType().Name}.{field.Name}");
				}
			}
		}
	}
}
