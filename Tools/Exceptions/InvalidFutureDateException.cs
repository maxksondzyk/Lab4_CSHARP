using System;

namespace KsondzykLab2.Tools.Exceptions
{
    internal class InvalidFutureDateException : ArgumentException
    {
        internal InvalidFutureDateException(string message) : base(message)
        {

        }
    }
}
