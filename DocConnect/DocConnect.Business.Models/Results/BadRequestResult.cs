using DocConnect.Business.Models.Enums;

namespace DocConnect.Business.Models.Results
{
    public class BadRequestResult<T> : IResult<T>
    {
        public BadRequestResult(params string[] errorMessages)
        {
            ErrorMessages = errorMessages ?? Enumerable.Empty<string>();
        }

        public DocConnectStatusCode StatusCode => DocConnectStatusCode.BadRequest;

        public IEnumerable<string> ErrorMessages { get; set; }
        public T Data { get; }
    }
}
