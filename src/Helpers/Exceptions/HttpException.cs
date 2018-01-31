using Nancy;
using System;

namespace Api.Helpers
{
    [Serializable]
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public object Content { get; private set; }

        public HttpException(HttpStatusCode statusCode, object content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public HttpException(HttpStatusCode statusCode) : this(statusCode, string.Empty)
        {
        }

        public HttpException() : this(HttpStatusCode.InternalServerError, string.Empty)
        {
        }

        public static HttpException NotFound(object content) => new HttpException(HttpStatusCode.NotFound, content);

        public static Exception InternalServerError(object content) => new HttpException(HttpStatusCode.InternalServerError, content);
    }
}
