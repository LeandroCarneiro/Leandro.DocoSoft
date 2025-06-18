
namespace SertaoArch.UserMi.Common.Exceptions
{
    public class InvalidObjectException : AppBaseException
    {
        public InvalidObjectException() : base("The system could not resolve data type") { }

        public InvalidObjectException(string msg) : base(msg) { }
    }
}
