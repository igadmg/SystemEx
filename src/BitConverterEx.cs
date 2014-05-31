using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SystemEx
{
	public static class BitConverterEx
	{
		private delegate byte[] GetBytesDelegate(object o);
		private delegate object GetTypeDelegate(byte[] value, int startIndex);
		private static Dictionary<Type, int> lengthByType = new Dictionary<Type, int>();
		private static Dictionary<Type, GetBytesDelegate> bytesByType = new Dictionary<Type, GetBytesDelegate>();
		private static Dictionary<Type, GetTypeDelegate> typeByType = new Dictionary<Type, GetTypeDelegate>();

		static BitConverterEx()
		{
			foreach (var m in typeof(BitConverter).GetMethods(BindingFlags.Static | BindingFlags.Public)) {
				if (m.Name == "GetBytes") {
					bytesByType.Add(m.GetParameters()[0].ParameterType, (object o) => (byte[])m.Invoke(null, new object[] { o }));
				}
				else if (m.Name.StartsWith("To") && m.GetParameters().Length == 2) {
					typeByType.Add(m.ReturnType, (byte[] value, int startIndex) => m.Invoke(null, new object[] { value, startIndex }));
				}
			}

			foreach (var p in bytesByType) {
				lengthByType.Add(p.Key, p.Value(Activator.CreateInstance(p.Key)).Length);
			}
		}

		public static byte[] GetBytes(object o)
		{
			GetBytesDelegate gbd;
			if (bytesByType.TryGetValue(o.GetType(), out gbd))
				return gbd(o);

			using (MemoryStream bytes = new MemoryStream(128)) {
				foreach (var field in o.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)) {
					bytes.Write(bytesByType[field.FieldType](field.GetValue(o)));
				}
				return bytes.ToArray();
			}
		}

		public static object FromBytes(Type type, byte[] value, ref int startIndex)
		{
			object o = null;

			GetTypeDelegate gtd;
			if (typeByType.TryGetValue(type, out gtd)) {
				o = gtd(value, startIndex);
				startIndex += lengthByType[type];
			}
			else {
				o = Activator.CreateInstance(type);

				foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)) {
					field.SetValue(o, FromBytes(field.FieldType, value, ref startIndex));
				}
			}

			return o;
		}

		public static T FromBytes<T>(byte[] value)
			where T : new()
		{
			int si = 0;
			return (T) FromBytes(typeof(T), value, ref si);
		}

		public static T FromBytes<T>(byte[] value, ref int startIndex)
			where T : new()
		{
			return (T) FromBytes(typeof(T), value, ref startIndex);
		}
	}
}
