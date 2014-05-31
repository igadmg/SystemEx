using System.IO;

namespace SystemEx
{
	public static class MemoryStreamEx
	{
		public static MemoryStream Write(this MemoryStream stream, byte[] buffer)
		{
			stream.Write(buffer, 0, buffer.Length);
			return stream;
		}
	}
}
