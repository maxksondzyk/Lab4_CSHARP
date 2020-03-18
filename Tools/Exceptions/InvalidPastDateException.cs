using System;

namespace KsondzykLab2.Tools.Exceptions
{
    internal class InvalidPastDateException : ArgumentException
    {
        internal InvalidPastDateException(string message) : base(message)
        {

        }
    }
}
