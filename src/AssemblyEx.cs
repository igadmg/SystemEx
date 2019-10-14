using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SystemEx
{
	public static class AssemblyEx
	{
		public static Assembly ExecutingAssembly { get { return Assembly.GetExecutingAssembly(); } }

		public static IEnumerable<Type> EnumNamespace(this Assembly assembly, string namespaceName)
		{
			foreach (var type in assembly.GetTypes())
			{
				if (type.FullName.StartsWith(namespaceName))
				{
					yield return type;
				}
			}

			yield break;
		}

		public static IEnumerable<dynamic> EnumTypesWithAttribute<A>(this Assembly assembly)
			 where A : Attribute
		{
			var assemblyTypes =
				from t in assembly.GetTypes()
				select new
				{
					Type = t,
					Attribute = t.GetAttribute<A>()
				};

			return
				from t in assemblyTypes
				where t.Attribute != null
				select t.ToExpando();
		}
	}
}
