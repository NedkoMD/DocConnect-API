using DocConnect.Business.Models.Enums;

namespace DocConnect.Business.Models.Results
{
    public class CreatedResult<T> : IResult<T>
    {
        public CreatedResult(T data)
        {
            Data = data;
        }

        public DocConnectStatusCode StatusCode => DocConnectStatusCode.Created;

        public IEnumerable<string> ErrorMessages => Enumerable.Empty<string>();

        public T Data { get; }
    }
}
