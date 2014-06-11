using System.IO;

namespace SystemEx
{
	public static class MemoryStreamEx
	{
		public static byte[] Read(this MemoryStream stream)
		{
			var data = new byte[stream.Length - stream.Position];
			stream.Read(data, 0, data.Length);
			return data;
		}

		public static MemoryStream Write(this MemoryStream stream, byte[] buffer)
		{
			stream.Write(buffer, 0, buffer.Length);
			return stream;
		}
	}
}
