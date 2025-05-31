using System;

namespace Leandro.DocoSoft.Common.Exceptions
{
    public class AppPersistenceException : AppBaseException
    {
        public AppPersistenceException() : base("The system could not persiste data") { }

        public AppPersistenceException(string msg) : base(msg) { }
    }
}
