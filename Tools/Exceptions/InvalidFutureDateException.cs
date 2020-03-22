using System;

namespace Lab4_CSHARP.Tools.Exceptions
{
    internal class InvalidFutureDateException : ArgumentException
    {
        internal InvalidFutureDateException(string message) : base(message)
        {

        }
    }
}
