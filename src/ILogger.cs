using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemEx
{
    public interface ILogger
    {
        void Log(string message, params object[] args);
    }

    public static partial class SystemEx
    {
        public static ILogger logger = new ConsoleLogger();
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message, params object[] args)
        {
            Console.WriteLine("Log: {0}", string.Format(message, args));
        }
    }
}
