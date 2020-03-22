using System;

namespace Lab4_CSHARP.Tools.Exceptions
{
    internal class InvalidPastDateException : ArgumentException
    {
        internal InvalidPastDateException(string message) : base(message)
        {

        }
    }
}
