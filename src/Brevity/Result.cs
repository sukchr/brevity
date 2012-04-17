namespace Brevity
{
    /// <summary>
    /// A simple class that represents the result of some operation or check.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// True if the operation/check was successfull. 
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// A meaningful message to explain the <see cref="IsSuccess"/> status.
        /// </summary>
        public string Message { get; set; }


        public static implicit operator Result(bool isSuccess)
        {
            return new Result { IsSuccess = isSuccess };
        }

        public static implicit operator bool(Result result)
        {
            return result.IsSuccess;
        }
    }
}