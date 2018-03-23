using System;

namespace SystemEx
{
	public interface IProgram
	{
		IAsyncResult Start();

		IAsyncResult Stop();

		void Update();
	}
}
