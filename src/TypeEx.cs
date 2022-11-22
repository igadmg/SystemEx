using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SystemEx
{
	public struct EnumNameValuePair<T>
	{
		public string Name;
		public T Value;
	}

	public struct EnumNameValuePairWithAttribute<T, A>
		where A : Attribute
	{
		public string Name;
		public T Value;
		public A Attribute;
	}

	public static class TypeEx
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static MethodBase GetCurrentMethod()
		{
			var st = new System.Diagnostics.StackTrace();
			var sf = st.GetFrame(1);

			return sf.GetMethod();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static MethodBase GetCallingMethod()
		{
			var st = new System.Diagnostics.StackTrace();
			var sf = st.GetFrame(2);

			return sf.GetMethod();
		}

		public static string SharpName(this Type type)
		{
			if (!type.IsGenericType)
				return type.FullName;

			string generic = type.GetGenericArguments().Select(tp => tp.SharpName()).Join(", ");

			string type_name = type.Name;
			var ti = type_name.LastIndexOf('`');
			if (ti < 0)
			{
				var dt = type.DeclaringType;
				while (dt != null)
				{
					var tn = dt.Namespace + "." + dt.Name;
					ti = tn.LastIndexOf('`');
					if (!(ti < 0))
					{
						type_name = string.Format("{0}<{1}>", tn.Substring(0, ti), generic) + "." + type_name;
					}

					dt = dt.DeclaringType;
				}
			}
			else
			{
				type_name = string.Format("{0}<{1}>", type.Namespace + "." + type_name.Substring(0, ti), generic);
			}

			return type_name;
		}

		public static bool IsList(this Type type)
		{
			return typeof(IList).IsAssignableFrom(type);
		}

		public static Type GetListItemType(this Type type)
		{
			if (type.IsArray)
				return type.GetElementType();

			var listType = type.GetInterfaces()
				.Where(i => i.IsGenericType)
				.Where(i => i.GetGenericTypeDefinition() == typeof(IList<>)).First();

			if (listType != null)
				return listType.GetGenericArguments()[0];

			return null;
		}

		public static bool IsDictionary(this Type type)
		{
			return typeof(IDictionary).IsAssignableFrom(type);
		}

		public static KeyValuePair<Type, Type> GetDictionaryKeyValueType(this Type type)
		{
			var dictionaryType = type.GetInterfaces()
				.Where(i => i.IsGenericType)
				.Where(i => i.GetGenericTypeDefinition() == typeof(IDictionary<,>)).First();

			if (dictionaryType != null)
				return new KeyValuePair<Type, Type>(dictionaryType.GetGenericArguments()[0], dictionaryType.GetGenericArguments()[1]);

			return new KeyValuePair<Type, Type>(null, null);
		}

		public static bool IsA<T>(this Type type)
			=> type.IsA(typeof(T));

		public static bool IsA(this Type type, Type t)
		{
			bool IsBothGenericType = type.IsGenericType && t.IsGenericType;
			bool IsAnyGenericTypeDefinition = type.IsGenericTypeDefinition || t.IsGenericTypeDefinition;

			if (IsBothGenericType)
			{
				if (!IsAnyGenericTypeDefinition)
				{
					var gtype = type.GetGenericTypeDefinition();
					var gt = t.GetGenericTypeDefinition();
					return gtype.IsA(gt)
						&& type.GenericTypeArguments
							.Zip(t.GenericTypeArguments, (a, b) => new { a, b })
							.All(p => p.a.IsA(p.b));
				}
				else
				{
					return type == t || type.IsSubclassOf(t) || type.GetInterfaces().Where(i => i.Name == t.Name).Any();
				}
			}
			else
				return type == t || type.BaseType == t || type.IsSubclassOf(t) || t.IsAssignableFrom(type);
		}

		[Obsolete("Use IsA")]
		public static bool IsSubclassOf<T>(this Type type)
			=> type.IsSubclassOf<T>();

		public static IEnumerable<Type> GetBaseTypes<StopType>(this Type type)
		{
			Type c = type;
			while (c != null && c != typeof(StopType))
			{
				yield return c;
				c = c.BaseType;
			}
		}

		public static bool HasAttribute<A>(this MemberInfo mi)
			where A : Attribute
		{
			foreach (var attribute in mi.GetCustomAttributes(true))
			{
				if (attribute.GetType() == typeof(A) || attribute.GetType().IsSubclassOf(typeof(A)))
				{
					return true;
				}
			}

			return false;
		}

		public static A GetAttribute<A>(this MemberInfo mi)
			where A : Attribute
		{
			foreach (var attribute in mi.GetCustomAttributes(true))
			{
				if (attribute.GetType() == typeof(A) || attribute.GetType().IsSubclassOf(typeof(A)))
				{
					return (A)attribute;
				}
			}

			return null;
		}

		public static IEnumerable<A> GetAttributes<A>(this MemberInfo mi)
			where A : Attribute
		{
			foreach (var attribute in mi.GetCustomAttributes(true))
			{
				if (attribute.GetType() == typeof(A) || attribute.GetType().IsSubclassOf(typeof(A)))
				{
					yield return (A)attribute;
				}
			}

			yield break;
		}

		/// <summary>
		/// Lists all private fields with attribute A.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		public static IEnumerable<FieldInfo> GetFields<A>(this Type t)
			where A : Attribute
		{
			foreach (var field in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (field.HasAttribute<A>())
					yield return field;
			}
			yield break;
		}

		public static IEnumerable<FieldInfo> GetFieldsOf<TType>(this Type t)
			=> t.GetFieldsOf(typeof(TType));

		public static IEnumerable<FieldInfo> GetFieldsOf(this Type t, Type type)
		{
			foreach (var field in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (field.FieldType.IsA(type))
					yield return field;
			}
			yield break;
		}

		/// <summary>
		/// Lists all private properties with attribute A.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		public static IEnumerable<PropertyInfo> GetProperties<A>(this Type t)
			where A : Attribute
		{
			foreach (var property in t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				if (property.HasAttribute<A>())
					yield return property;
			}
			yield break;
		}

		/// <summary>
		/// Lists all private fields with attribute A.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		public static IEnumerable<Tuple<FieldInfo, A>> GetFieldsAndAttributes<A>(this Type t, BindingFlags flags)
			where A : Attribute
		{
			foreach (var field in t.GetFields(flags))
			{
				A a = field.GetAttribute<A>();
				if (a != null)
					yield return new Tuple<FieldInfo, A>(field, a);
			}
			yield break;
		}

		public static IEnumerable<Tuple<FieldInfo, A>> GetFieldsAndAttributes<A>(this Type t)
			where A : Attribute
			=> t.GetFieldsAndAttributes<A>(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

		public static IEnumerable<MethodInfo> GetMethods(this Type t, string name)
		{
			foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
			{
				if (method.Name == name)
					yield return method;
			}
			yield break;
		}

		/// <summary>
		/// Lists all private methods with attribute A.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		public static IEnumerable<MethodInfo> GetMethods<A>(this Type t)
			where A : Attribute
		{
			foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				if (method.HasAttribute<A>())
					yield return method;
			}
			yield break;
		}

		public static IEnumerable<Tuple<MethodInfo, A>> GetMethodsAndAttributes<A>(this Type t)
			where A : Attribute
		{
			foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				A a = method.GetAttribute<A>();
				if (a != null)
					yield return new Tuple<MethodInfo, A>(method, a);
			}
			yield break;
		}

		/// <summary>
		/// Get method with exact name and exact parameter types.
		/// </summary>
		/// <param name="t"></param>
		/// <param name="name"></param>
		/// <param name="arguments"></param>
		/// <returns></returns>
		public static MethodInfo GetMethod(this Type t, string name, params Type[] arguments)
		{
			bool isGeneric = false;
			int i = name.IndexOf('`');
			if (i > 0)
			{
				isGeneric = true;
				name = name.Substring(0, i);
			}

			foreach (var method in t.GetMethods(name))
			{
				if (method.IsGenericMethod != isGeneric)
					continue;

				if (method.GetParameters().Compare(arguments, (ParameterInfo a, Type b) => a.ParameterType == b))
					return method;
			}

			return null;
		}

		public static bool HasInterface(this Type type, Type iface)
		{
			if (!iface.IsGenericType)
				return type.GetInterface(iface.Name) != null;

			foreach (var itype in type.GetInterfaces())
			{
				if (itype.IsGenericType && itype.GetGenericTypeDefinition() == iface)
					return true;
			}

			return false;
		}

		public static bool HasInterface<I>(this Type type)
		{
			return type.HasInterface(typeof(I));
		}

		public static string id(this Enum e)
		{
			return "{0}.{1}".format(e.GetType().FullName, e.ToString());
		}

		public static FieldInfo field(this string s)
		{
			string[] names = s.Split(new[] { ',' }, 2);

			int lastDot = names[0].LastIndexOf('.');
			return Type.GetType(names[1])
				?.GetField(names[0].Substring(lastDot + 1));
		}

		public static FieldInfo field(this Enum e)
			=> e.GetType().GetField(e.ToString());

		public static IEnumerable<FieldAttributePair<A>> EnumFieldsWithAttribute<A>(this Type type, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
			where A : Attribute
		{
			return type.GetFields(bindingAttr)
				.Select(field => new FieldAttributePair<A> {
					Field = field,
					Attribute = field.GetAttribute<A>()
				})
				.Where(field => field.Attribute != null);
		}

		public static IEnumerable<MethodAttributePair<A>> EnumMethodsWithAttribute<A>(this Type type, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
			where A : Attribute
		{
			return type.GetMethods(bindingAttr)
				.Select(method => new MethodAttributePair<A> {
					Method = method,
					Attribute = method.GetAttribute<A>()
				})
				.Where(method => method.Attribute != null);
		}

		public static IEnumerable<MethodAttributePair<A>> EnumMethodsWithMultipleAttribute<A>(this Type type, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
			where A : Attribute
		{
			return type.GetMethods(bindingAttr)
				.Select(method => new
				{
					Method = method,
					Attributes = method.GetAttributes<A>().ToArray()
				})
				.Where(method => method.Attributes.Length > 0)
				.SelectMany(method => method.Attributes.Select(a => new MethodAttributePair<A>
				{
					Method = method.Method,
					Attribute = a
				}));
		}

		public static IEnumerable<EnumNameValuePair<T>> EnumEnumValues<T>(this Type type)
		{
			return type.GetFields()
				.Where(i => i.FieldType.IsEnum)
				.Select(i => new EnumNameValuePair<T> {
					Name = i.Name,
					Value = (T)i.GetRawConstantValue()
				});
		}

		public static IEnumerable<EnumNameValuePairWithAttribute<T, A>> EnumEnumValuesWithAttribute<T, A>(this Type type)
			where A : Attribute
		{
			return type.GetFields()
				.Where(i => i.FieldType.IsEnum)
				.Select(i => new EnumNameValuePairWithAttribute<T, A> {
					Name = i.Name,
					Value = (T)i.GetRawConstantValue(),
					Attribute = i.GetAttribute<A>()
				})
				.Where(i => i.Attribute != null);
		}

		public static IEnumerable<EnumNameValuePair<T>> EnumEnumValuesWithoutAttribute<T, A>(this Type type)
			where A : Attribute
		{
			return type.GetFields()
				.Where(i => i.FieldType.IsEnum)
				.Select(i => new EnumNameValuePairWithAttribute<T, A> {
					Name = i.Name,
					Value = (T)i.GetRawConstantValue(),
					Attribute = i.GetAttribute<A>()
				})
				.Where(i => i.Attribute == null)
				.Select(i => new EnumNameValuePair<T> {
					Name = i.Name,
					Value = i.Value
				});
		}
	}
}
