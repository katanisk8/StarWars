using CSharpFunctionalExtensions;

namespace CodePepper.Domain
{
    public class OperationResult
    {
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
    }

    public static class ResultExtensions
    {
        public static OperationResult ToOperationResult(this Result result)
        {
            return new OperationResult
            {
                IsFailure = result.IsFailure,
                IsSuccess = result.IsSuccess,
                Error = result.IsFailure ? result.Error : "",
            };
        }
    }
}
