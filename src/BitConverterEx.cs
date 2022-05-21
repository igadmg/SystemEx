using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SystemEx
{
	public static class BitConverterEx
	{
		private delegate byte[] GetBytesDelegate(object o);
		private delegate object GetTypeDelegate(byte[] data, int startIndex);

		private static Dictionary<Type, int> lengthByType = new Dictionary<Type, int>();

		private static Dictionary<Type, ITypeBitConverter> typeDescriptions = new Dictionary<Type, ITypeBitConverter>();


		private interface ITypeBitConverter
		{
			int Length { get; }
			byte[] GetBytes(object o);
			object FromBytes(byte[] data, int startIndex);
		}

		private class SimpleTypeBitConverter : ITypeBitConverter
		{
			private GetBytesDelegate getBytes;
			private GetTypeDelegate fromBytes;

			public SimpleTypeBitConverter(GetBytesDelegate getBytes, GetTypeDelegate fromBytes, int length)
			{
				this.getBytes = getBytes;
				this.fromBytes = fromBytes;
				this.Length = length;
			}

			public int Length { get; protected set; }
			public byte[] GetBytes(object o) { return getBytes(o); }
			public object FromBytes(byte[] data, int startIndex) { return fromBytes(data, startIndex); }
		}

		private class TypeDesciption : ITypeBitConverter
		{
			private Type type;
			private List<Tuple<FieldInfo, ITypeBitConverter>> fields = new List<Tuple<FieldInfo, ITypeBitConverter>>();

			public static TypeDesciption Parse(Type type)
			{
				TypeDesciption td = new TypeDesciption();

				td.type = type;
				td.Length = 0;
				foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
				{
					if (field.FieldType.IsEnum)
					{
						var enum_type = Enum.GetUnderlyingType(field.FieldType);
						ITypeBitConverter tbc;
						if (!typeDescriptions.TryGetValue(enum_type, out tbc))
						{
							throw new Exception("Unknown enum underlying type '{0}'".format(enum_type.FullName));
						}
						td.fields.Add(Tuple.Create(field, tbc));
						td.Length += tbc.Length;
					}
					else
					{
						ITypeBitConverter tbc;
						if (!typeDescriptions.TryGetValue(field.FieldType, out tbc))
						{
							tbc = TypeDesciption.Parse(field.FieldType);
							typeDescriptions.Add(field.FieldType, tbc);
						}
						td.fields.Add(Tuple.Create(field, tbc));
						td.Length += tbc.Length;
					}
				}

				return td;
			}

			public int Length { get; protected set; }
			public byte[] GetBytes(object o)
			{
				using var bytes = new MemoryStream(Length);
				foreach (var field in fields)
					bytes.Write(field.Item2.GetBytes(field.Item1.GetValue(o)));

				return bytes.ToArray();
			}

			public object FromBytes(byte[] data, int startIndex)
			{
				var o = Activator.CreateInstance(type);

				foreach (var field in fields)
				{
					field.Item1.SetValue(o, field.Item2.FromBytes(data, startIndex));
					startIndex += field.Item2.Length;
				}

				return o;
			}
		}

		static BitConverterEx()
		{
			Dictionary<Type, GetBytesDelegate> bytesByType = new Dictionary<Type, GetBytesDelegate>();
			Dictionary<Type, GetTypeDelegate> typeByType = new Dictionary<Type, GetTypeDelegate>();

			foreach (var m in typeof(BitConverter).GetMethods(BindingFlags.Static | BindingFlags.Public))
			{
				var method = m;
				if (m.Name == "GetBytes")
				{
					bytesByType.Add(m.GetParameters()[0].ParameterType, (o) => (byte[])method.Invoke(null, new object[] { o }));
				}
				else if (m.Name.StartsWith("To") && m.GetParameters().Length == 2 && !m.Name.StartsWith("ToString"))
				{
					typeByType.Add(m.ReturnType, (data, si) => method.Invoke(null, new object[] { data, si }));
				}
			}

			foreach (var key in bytesByType.Keys)
			{
				typeDescriptions.Add(key, new SimpleTypeBitConverter(bytesByType[key], typeByType[key], bytesByType[key](Activator.CreateInstance(key)).Length));
			}
		}

		public static void RegisterType(Type type)
		{
			ITypeBitConverter tbc;
			if (!typeDescriptions.TryGetValue(type, out tbc))
			{
				tbc = TypeDesciption.Parse(type);
				typeDescriptions.Add(type, tbc);
			}
			else
			{
				Log.Error("Type '{0}' is already registered in BitConverterEx.", type.FullName);
			}
		}

		public static byte[] GetBytes(this object o)
		{
			Type type = o.GetType();

			ITypeBitConverter tbc;
			if (!typeDescriptions.TryGetValue(type, out tbc))
			{
				tbc = TypeDesciption.Parse(type);
				typeDescriptions.Add(type, tbc);
			}

			return tbc.GetBytes(o);
		}

		public static object FromBytes(Type type, byte[] value, ref int startIndex)
		{
			ITypeBitConverter tbc;
			if (!typeDescriptions.TryGetValue(type, out tbc))
			{
				tbc = TypeDesciption.Parse(type);
				typeDescriptions.Add(type, tbc);
			}

			object o = tbc.FromBytes(value, startIndex);
			startIndex += tbc.Length;
			return o;
		}

		public static T FromBytes<T>(this byte[] value)
			where T : new()
		{
			int si = 0;
			return (T)FromBytes(typeof(T), value, ref si);
		}

		public static T FromBytes<T>(this byte[] value, ref int startIndex)
			where T : new()
		{
			return (T)FromBytes(typeof(T), value, ref startIndex);
		}
	}
}
