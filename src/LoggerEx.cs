using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemEx
{
    public static class LoggerEx
    {
        public static ILogger logger = new ConsoleLogger();

		public static void Log(string message, params object[] args)
		{
			logger.Log(message, args);
		}
    }

	public interface ILogger
	{
		void Log(string message, params object[] args);
	}

	public class ConsoleLogger : ILogger
    {
        public void Log(string message, params object[] args)
        {
            Console.WriteLine("Log: {0}", string.Format(message, args));
        }
    }
}
