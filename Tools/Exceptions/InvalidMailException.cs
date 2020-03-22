using System;

namespace Lab4_CSHARP.Tools.Exceptions
{
    internal class InvalidMailException : ArgumentException
    {
        internal InvalidMailException(string message) : base(message)
        {

        }
    }
}
