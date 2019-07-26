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
	}
}
