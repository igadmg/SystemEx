using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemEx
{
    public static class LoggerEx
    {
        public static ILogger logger = new ConsoleLogger();

		public static void Init(ILogger logger)
		{
			LoggerEx.logger = logger;
		}

		public static void Log(string message, params object[] args)
		{
			logger.Log(message, args);
		}

		public static void LogWarning(string message, params object[] args)
		{
			logger.LogWarning(message, args);
		}

		public static void LogError(string message, params object[] args)
		{
			logger.LogError(message, args);
		}
    }

	public interface ILogger
	{
		void Log(string message, params object[] args);
		void LogWarning(string message, params object[] args);
		void LogError(string message, params object[] args);
	}

	public class ConsoleLogger : ILogger
    {
        public void Log(string message, params object[] args)
        {
            Console.WriteLine("Log: {0}", string.Format(message, args));
        }

		public void LogWarning(string message, params object[] args)
		{
			Console.WriteLine("Warning: {0}", string.Format(message, args));
		}

		public void LogError(string message, params object[] args)
		{
			Console.WriteLine("Error: {0}", string.Format(message, args));
		}
    }
}
