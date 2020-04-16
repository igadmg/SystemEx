using System;
using System.Runtime.InteropServices;

namespace SystemEx
{
	public class StructStream : IDisposable
	{
		byte[] data_;
		int length_;

		GCHandle handle;
		IntPtr dataPtr;


		public int Capacity {
			get { return data_.Length; }
			set {
				handle.Free();

				Array.Resize(ref data_, value);

				handle = GCHandle.Alloc(data_, GCHandleType.Pinned);
				dataPtr = handle.AddrOfPinnedObject();
			}
		}
		public int Length { get { return length_; } }
		public bool isEOF { get { return length_ == data_.Length; } }


		public StructStream()
			: this(new byte[512])
		{
		}

		public StructStream(byte[] data)
		{
			data_ = data;
			length_ = 0;

			handle = GCHandle.Alloc(data_, GCHandleType.Pinned);
			dataPtr = handle.AddrOfPinnedObject();
		}

		public void Dispose()
		{
			handle.Free();
		}

		public object Read(Type T)
		{
			object r = Marshal.PtrToStructure(dataPtr, T);
			Skip(T);
			return r;
		}

		public T Read<T>() where T : struct
		{
			return (T)Read(typeof(T));
		}

		public T[] Read<T>(int count) where T : struct
		{
			T[] r = new T[count];
			for (int i = 0; i < r.Length; i++)
				r[i] = Read<T>();
			return r;
		}

		public StructStream Write(object o, Type T)
		{
			if (length_ + Marshal.SizeOf(T) > Capacity)
			{
				Capacity = Capacity * 2;
			}

			Marshal.StructureToPtr(o, dataPtr, false);
			Skip(T);
			return this;
		}

		public StructStream Write<T>(T o) where T : struct
		{
			return Write(o, typeof(T));
		}

		public void Skip(Type T)
		{
			Skip(T, 1);
		}

		public void Skip(Type T, int count)
		{
			int delta = count * Marshal.SizeOf(T);
			dataPtr = new IntPtr(dataPtr.ToInt64() + delta);
			length_ += delta;
		}

		public void Skip<T>()
		{
			Skip(typeof(T), 1);
		}

		public void Skip<T>(int count)
		{
			Skip(typeof(T), count);
		}

		public byte[] ToArray()
		{
			return data_;
		}
	}
}
