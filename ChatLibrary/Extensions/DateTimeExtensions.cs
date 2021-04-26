using System;

namespace ChatLibrary.Extensions
{
    internal static class DateTimeExtensions
    {
        internal static string ToTimestampString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}