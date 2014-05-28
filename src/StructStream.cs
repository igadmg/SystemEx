using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace SystemEx
{
	public class StructStream : IDisposable
	{
		MemoryStream stream;
        BinaryFormatter bf = new BinaryFormatter();

        public StructStream()
        {
            stream = new MemoryStream(512);
        }

		public StructStream(byte[] data)
		{
            SystemEx.logger.Log("new stream of {0} bytes", data.Length);
			stream = new MemoryStream(data);
		}

		public void Dispose()
		{
			stream.Dispose();
		}
		

		public T Read<T>() where T : struct
		{
            object o = bf.Deserialize(stream);
            T r = (T) o;
			return r;
		}

		public T[] Read<T>(int count) where T : struct
		{
			T[] r = new T[count];
			for (int i = 0; i < r.Length; i++)
				r[i] = Read<T>();
			return r;
		}

        public StructStream Write<T>(T o) where T : struct
        {
            bf.Serialize(stream, o);
            SystemEx.logger.Log("write {0} of {1} bytes", typeof(T).Name, stream.Length);
            return this;
        }

		public void Skip<T>()
		{
            Skip<T>(1);
		}

		public void Skip<T>(int count)
		{
			stream.Position += count * Marshal.SizeOf(typeof(T));
		}

        public byte[] ToArray()
        {
            return stream.ToArray();
        }
	}
}
