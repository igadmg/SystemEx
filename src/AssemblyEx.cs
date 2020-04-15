using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SystemEx
{
	public static class AssemblyEx
	{
		public static Assembly ExecutingAssembly { get { return Assembly.GetExecutingAssembly(); } }

		public static IEnumerable<Type> EnumNamespace(this Assembly assembly, string namespaceName)
		{
			return assembly.GetTypes()
				.Where(t => t.FullName.StartsWith(namespaceName));
		}

		public static IEnumerable<TypeAttributePair<A>> EnumTypesWithAttribute<A>(this Assembly assembly)
			 where A : Attribute
		{
			return assembly.GetTypes()
				.Select(t => new TypeAttributePair<A>
				{
					Type = t,
					Attribute = t.GetAttribute<A>()
				})
				.Where(t => t.Attribute != null);
		}
	}
}
