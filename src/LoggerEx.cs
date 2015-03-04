using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SystemEx
{
    public static class Log
    {
        public static ILogger logger = new ConsoleLogger();

		public static void Init(ILogger logger)
		{
			Log.logger = logger;
		}

		public static void Info(string message, params object[] args)
		{
			logger.Info(message, args);
		}

		public static void Warning(string message, params object[] args)
		{
			logger.Warning(message, args);
		}

		public static void Error(string message, params object[] args)
		{
			logger.Error(message, args);
		}
    }

	public interface ILogger
	{
		void Info(string message, params object[] args);
		void Warning(string message, params object[] args);
		void Error(string message, params object[] args);
	}

	public class ConsoleLogger : ILogger
    {
        public void Info(string message, params object[] args)
        {
			Console.WriteLine("Log: {0}", message.format(args));
        }

		public void Warning(string message, params object[] args)
		{
			Console.WriteLine("Warning: {0}", message.format(args));
		}

		public void Error(string message, params object[] args)
		{
			Console.WriteLine("Error: {0}", message.format(args));
		}
    }

	public class StreamLogger : ILogger
	{
		private StreamWriter stream;


		public StreamLogger(Stream stream)
		{
			this.stream = new StreamWriter(stream);
			this.stream.AutoFlush = true;
		}

		public StreamLogger(StreamWriter stream)
		{
			this.stream = stream;
			this.stream.AutoFlush = true;
		}


		public void Info(string message, params object[] args)
		{
			stream.WriteLine("Log: {0}", message.format(args));
		}

		public void Warning(string message, params object[] args)
		{
			stream.WriteLine("Warning: {0}", message.format(args));
		}

		public void Error(string message, params object[] args)
		{
			stream.WriteLine("Error: {0}", message.format(args));
		}
	}
}
