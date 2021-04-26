using System;

namespace ChatLibrary.Helpers
{
    /// <summary>
    /// Just an easy way to throw validations and apply "Fail First" approach
    /// </summary>
    internal static class ThrowExHelper
    {
        internal static void ThrowException(string msg)
        {
            throw new Exception("[EXCEPTION]" + msg);
        }

        internal static void ThrowValidationException(string msg)
        {
            throw new Exception("[VALIDATION]" + msg);
        }
    }
}