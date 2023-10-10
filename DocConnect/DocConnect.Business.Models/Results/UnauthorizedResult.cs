using DocConnect.Business.Models.Enums;

namespace DocConnect.Business.Models.Results
{
    public class UnauthorizedResult<T> : IResult<T>
    {
        public UnauthorizedResult(params string[] errorMessages)
        {
            ErrorMessages = errorMessages ?? Enumerable.Empty<string>();
        }

        public DocConnectStatusCode StatusCode => DocConnectStatusCode.Unauthorized;

        public IEnumerable<string> ErrorMessages { get; set; }
        public T Data { get; }
    }
}
