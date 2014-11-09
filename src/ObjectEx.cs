using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SystemEx
{
	public static class ObjectEx
	{
		public static T GetPropertyValue<T>(this object o, string name)
		{
			return (T) o.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(o, null);
		}
	}
}
