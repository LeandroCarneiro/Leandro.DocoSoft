using System;

namespace SertaoArch.UserMi.Common.Exceptions
{
    public class AppBaseException : Exception
    {
        public AppBaseException(string msg) : base(msg) { }
    }
}
