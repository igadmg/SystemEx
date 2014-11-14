using System;
using System.Collections.Generic;
using System.Reflection;

namespace SystemEx
{
	public static class TypeEx
	{
		public static string SharpName(this Type type)
		{
			if (!type.IsGenericType)
				return type.FullName;

			string generic = "";
			foreach (var t in type.GetGenericArguments())
				generic += t.SharpName() + ", ";
			generic = generic.TrimEnd(',', ' ');

			string type_name = type.Name;
			var ti = type_name.LastIndexOf('`');
			if (ti < 0) {
				var dt = type.DeclaringType;
				while (dt != null) {
					var tn = dt.Namespace + "." + dt.Name;
					ti = tn.LastIndexOf('`');
					if (!(ti < 0)) {
						type_name = string.Format("{0}<{1}>", tn.Substring(0, ti), generic) + "." + type_name;
					}

					dt = dt.DeclaringType;
				}
			}
			else {
				type_name = string.Format("{0}<{1}>", type.Namespace + "." + type_name.Substring(0, ti), generic);
			}

			return type_name;
		}

		public static bool HasAttribute<A>(this MemberInfo mi) where A : Attribute
		{
			foreach (var attribute in mi.GetCustomAttributes(true)) {
				if (attribute.GetType() == typeof(A)) {
					return true;
				}
			}

			return false;
		}

		public static A GetAttribute<A>(this MemberInfo mi) where A : Attribute
		{
			foreach (var attribute in mi.GetCustomAttributes(true)) {
				if (attribute.GetType() == typeof(A)) {
					return (A)attribute;
				}
			}

			return null;
		}

		public static IEnumerable<A> GetAttributes<A>(this MemberInfo mi) where A : Attribute
		{
			foreach (var attribute in mi.GetCustomAttributes(true)) {
				if (attribute.GetType() == typeof(A)) {
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
		public static IEnumerable<FieldInfo> GetFields<A>(this Type t) where A : Attribute
		{
			foreach (var field in t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				if (field.HasAttribute<A>())
					yield return field;
			}
			yield break;
		}

		/// <summary>
		/// Lists all private fields with attribute A.
		/// </summary>
		/// <typeparam name="A"></typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
		public static IEnumerable<Tuple<FieldInfo, A>> GetFieldsAndAttributes<A>(this Type t) where A : Attribute
		{
			foreach (var field in t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				A a = field.GetAttribute<A>();
				if (a != null)
					yield return new Tuple<FieldInfo, A>(field, a);
			}
			yield break;
		}

		public static IEnumerable<MethodInfo> GetMethods(this Type t, string name)
		{
			foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
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
		public static IEnumerable<MethodInfo> GetMethods<A>(this Type t) where A : Attribute
		{
			foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				if (method.HasAttribute<A>())
					yield return method;
			}
			yield break;
		}

		public static IEnumerable<Tuple<MethodInfo, A>> GetMethodsAndAttributes<A>(this Type t) where A : Attribute
		{
			foreach (var method in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
				A a = method.GetAttribute<A>();
				if (a != null)
					yield return new Tuple<MethodInfo, A>(method, a);
			}
			yield break;
		}

		public static MethodInfo GetMethod(this Type t, string name, params Type[] arguments)
		{
			bool isGeneric = false;
			int i = name.IndexOf('`');
			if (i > 0) {
				isGeneric = true;
				name = name.Substring(0, i);
			}

			foreach (var method in t.GetMethods(name)) {
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

			foreach (var itype in type.GetInterfaces()) {
				if (itype.IsGenericType && itype.GetGenericTypeDefinition() == iface)
					return true;
			}

			return false;
		}

		public static bool HasInterface<I>(this Type type)
		{
			return type.HasInterface(typeof(I));
		}
	}
}
