using System;

namespace Leandro.DocoSoft.Common.InternalObjects
{
    public abstract class BaseResult
    {
        public string Message { get; set; }
        public bool HasError { get; set; }
        public Exception Ex { get; set; }

        public BaseResult(string errorMessage, Exception ex = null)
        {
            HasError = true;
            Message = errorMessage;
            Ex = ex;
        }
    }

    public class AppResult<T> : BaseResult
    {
        public T Result { get; set; }

        public AppResult(T result) : base("")
        {
            Result = result;
            HasError = false;
        }

        public AppResult(string errorMessage, Exception ex = null) : base(errorMessage, ex)
        {
        }
    }

    public class AppResult : AppResult<object>
    {
        public AppResult(string errorMessage, Exception ex = null) : base(errorMessage, ex)
        {
        }
    }
}
