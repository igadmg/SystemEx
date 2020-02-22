using System.IO;

namespace SystemEx
{
	public static class StreamEx
	{
		public static Stream CopyTo(this Stream input, Stream output)
		{
			byte[] buffer = new byte[32768];
			int read;
			while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, read);
			}

			return input;
		}
	}
}
