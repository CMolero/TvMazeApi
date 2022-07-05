using System.Net;

namespace TvMazeApi.Repository.Exceptions
{
    public class UnexpectedResponseStatusException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; }

        public UnexpectedResponseStatusException(HttpStatusCode statusCode)
            : this($"Unexpected response status code: \"{statusCode}\"", statusCode)
        {

        }

        public UnexpectedResponseStatusException(string message, HttpStatusCode statusCode, Exception innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
