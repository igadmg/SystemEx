using System;
using System.Collections;
using System.Collections.Generic;

namespace SystemEx
{
	public static class DisposeEx
	{
		public static void Dispose(this IEnumerable<IDisposable> items)
		{
			foreach (var item in items)
				item.Dispose();
		}

		public static void Dispose(this ICollection<IDisposable> items)
		{
			foreach (var item in items)
				item.Dispose();

			items.Clear();
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
				else if (field.FieldType.IsA<ICollection<IDisposable>>())
				{
					var list = field.GetValue(o) as IEnumerable;
					if (list == null) continue;

					foreach (object p in list)
					{
						(p as IDisposable)?.Dispose();
					}
				}
				else if (field.FieldType.IsA<IEnumerable<IDisposable>>())
				{
					var list = field.GetValue(o) as IEnumerable;
					if (list == null) continue;

					foreach (object p in list)
					{
						(p as IDisposable)?.Dispose();
					}
				}
				else if (field.FieldType.IsA<Lazy<IDisposable>>())
				{
					var lazy = field.GetValue(o);
					if (lazy.GetPropertyValue<bool>("IsValueCreated"))
					{
						lazy.GetFieldValue<IDisposable>("Value")?.Dispose();
					}
				}
				else if (field.FieldType.IsA<IDisposable>())
				{
					(field.GetValue(o) as IDisposable).Elvis(d => d.Dispose());
				}
				else
				{
#if UNITY
					UnityEngine.Debug.LogWarning($"Don't know how to dispose field: {field.FieldType.Name} {o.GetType().Name}.{field.Name}");
#endif
				}
			}
		}
	}
}
