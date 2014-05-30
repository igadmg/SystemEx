using System;
using System.Runtime.InteropServices;

namespace SystemEx
{
    public class StructStream : IDisposable
    {
        byte[] data_;

        GCHandle handle;
        IntPtr dataPtr;


        public StructStream()
            : this(new byte[512])
        {
        }

        public StructStream(byte[] data)
        {
            data_ = data;

            handle = GCHandle.Alloc(data_, GCHandleType.Pinned);
            dataPtr = handle.AddrOfPinnedObject();
        }

        public void Dispose()
        {
            handle.Free();
        }


        public T Read<T>() where T : struct
        {
            T r = (T)Marshal.PtrToStructure(dataPtr, typeof(T));
            Skip<T>();
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
            Marshal.StructureToPtr(o, dataPtr, false);
            Skip<T>();
            return this;
        }

        public void Skip<T>()
        {
            Skip<T>(1);
        }

        public void Skip<T>(int count)
        {
            dataPtr = new IntPtr(dataPtr.ToInt64() + count * Marshal.SizeOf(typeof(T)));
        }

        public byte[] ToArray()
        {
            return data_;
        }
    }
}
