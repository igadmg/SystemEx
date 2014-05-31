using System;
using System.Runtime.InteropServices;

namespace SystemEx
{
	public static class MarshalEx
	{
		public static int SizeOf<T>() { return Marshal.SizeOf(typeof(T)); }

		public static T PtrToStructure<T>(IntPtr data) { return (T) Marshal.PtrToStructure(data, typeof(T)); }


		public static T BytesToStructure<T>(byte[] data)
		{
			GCHandle pdata = GCHandle.Alloc(data, GCHandleType.Pinned);
			var r = MarshalEx.PtrToStructure<T>(pdata.AddrOfPinnedObject());
			pdata.Free();
			return r;
		}

		public static byte[] StructureToBytes<T>(T o)
        {
            byte[] data = new byte[MarshalEx.SizeOf<T>()];
			GCHandle pdata = GCHandle.Alloc(data, GCHandleType.Pinned);
			Marshal.StructureToPtr(o, pdata.AddrOfPinnedObject(), false);
			pdata.Free();
			return data;
		}

		public static byte[] StructureToBytes<T>(T o, ref byte[] data)
		{
			GCHandle pdata = GCHandle.Alloc(data, GCHandleType.Pinned);
			Marshal.StructureToPtr(o, pdata.AddrOfPinnedObject(), false);
			pdata.Free();
			return data;
		}
	}
}
