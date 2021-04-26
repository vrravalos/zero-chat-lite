using System;

namespace ChatLibrary.Logging
{
    public static class SimpleConsoleLog 
    {
        
        // 0: date
        // 1: log level
        // 2: message
        private const string TEMPLATE_LOG = "{0} {1} {2}";

        public static void Log(LogLevel logLevel, string message)
        {
            // Filtering DEBUG
            if(logLevel == LogLevel.DEBUG && !System.Diagnostics.Debugger.IsAttached)
            {
                return;
            }

            Console.WriteLine(string.Format(TEMPLATE_LOG, DateTime.Now.ToString("HH:mm:ss.fff"), logLevel, message));
        }
    }
}