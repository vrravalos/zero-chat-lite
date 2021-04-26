namespace ChatLibrary.Logging
{
    public static class Logger
    {


        // Just a preference style:
        // I like to use 'using static' feature
        // and be able to call the log simple as that:
        // LOG(LogLevel.VERBOSE, "Hi");
        public static void LOG(LogLevel logLevel, string message)
        {
            // Here we can extend to use filters, use dependence injection, etc..
            // Now just creating a simple console log that works
            SimpleConsoleLog.Log(logLevel, message);
        }
    }
}