using DocConnect.Business.Models.Enums;

namespace DocConnect.Business.Models.Results
{
    public class NoContentResult<T> : IResult<T>
    {
        public DocConnectStatusCode StatusCode => DocConnectStatusCode.NoContent;

        public IEnumerable<string> ErrorMessages => Enumerable.Empty<string>();

        public T Data { get; }
    }
}
