using DocConnect.Business.Models.Enums;

namespace DocConnect.Business.Models.Results
{
    public class NotFoundResult<T> : IResult<T>
    {
        public NotFoundResult(params string[] errorMessages)
        {
            ErrorMessages = errorMessages ?? Enumerable.Empty<string>();
        }

        public DocConnectStatusCode StatusCode => DocConnectStatusCode.NotFound;

        public IEnumerable<string> ErrorMessages { get; set; }
        public T Data { get; }
    }
}
