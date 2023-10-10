using DocConnect.Business.Models.Enums;

namespace DocConnect.Business.Models.Results
{
    public class OkResult<T> : IResult<T>
    {
        public OkResult(T data)
        {
            Data = data;
        }

        public DocConnectStatusCode StatusCode => DocConnectStatusCode.OK;
        public IEnumerable<string> ErrorMessages => Enumerable.Empty<string>();
        public T Data { get; }
    }
}
