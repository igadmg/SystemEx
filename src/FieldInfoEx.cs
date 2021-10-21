using System.Reflection;

namespace SystemEx
{
	public static class FieldInfoEx
	{
		public static T GetValue<T>(this FieldInfo fi, object o)
			=> (T)fi.GetValue(o);
	}
}
