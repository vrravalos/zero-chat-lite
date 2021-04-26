using System.Collections.Generic;
using System.Linq;

namespace ZeroChatLite.Helpers
{
    public static class ArgsHelper
    {
        internal static Dictionary<string, string> GetParams(string[] args)
        {
            string port = "33000";

            foreach (var a in args)
            {
                // is number, so the 'port'
                if (a.All(char.IsDigit))
                {
                    port = a;
                }
            }

            return new Dictionary<string, string>
            {
                { "port", port}
            };
        }
    }
}