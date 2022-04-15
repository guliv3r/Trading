using System.Net;

namespace Trading.Core.Models.Response
{
    public class Response
    {
        public sealed class Successed<T> : Response
        {

            public Successed(T data)
            {
                Data = data;
            }
            public T Data { get; private set; }
            public Boolean Success { get; private set; } = true;
            public HttpStatusCode HttpStatusCode { get; private set; } = HttpStatusCode.OK;
        }

        public sealed class Failed : Response
        {
            public Failed(string errorCode, string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            {
                ErrorCode = errorCode;
                ErrorMessage = errorMessage;
                HttpStatusCode = httpStatusCode;
            }

            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
            public HttpStatusCode HttpStatusCode { get; set; }
            public Boolean Success { get; private set; } = false;
        }
    }
}
