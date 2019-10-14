using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;



namespace SystemEx
{
	public static class DynamicEx
	{
		public static ExpandoObject ToExpando(this object anonymousObject)
		{
			IDictionary<string, object> expando = new ExpandoObject();
			foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(anonymousObject))
			{
				var obj = propertyDescriptor.GetValue(anonymousObject);
				expando.Add(propertyDescriptor.Name, obj);
			}

			return (ExpandoObject)expando;
		}
	}
}
