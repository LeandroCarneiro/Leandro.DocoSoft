using System;

namespace SertaoArch.Common.Exceptions
{
    public class AppBaseException : Exception
    {
        public AppBaseException(string msg) : base(msg) { }
    }
}
