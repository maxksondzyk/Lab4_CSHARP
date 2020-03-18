using System;

namespace KsondzykLab2.Tools.Exceptions
{
    internal class InvalidMailException : ArgumentException
    {
        internal InvalidMailException(string message) : base(message)
        {

        }
    }
}
