using System.Net;
using System.Net.Sockets;

namespace SystemEx
{
	public static class UdpClientEx	
	{
		public static void Send(this UdpClient socket, byte[] data)
		{
			socket.Send(data, data.Length);
		}

		public static void Send(this UdpClient socket, byte[] data, IPEndPoint endpont)
		{
			socket.Send(data, data.Length, endpont);
		}
	}
}
