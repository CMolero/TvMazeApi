using System.Net;
using TvMazeApi.Repository.Exceptions;

namespace TvMazeApi.Repository.Strategy
{
    public class RateLimiterExceptionHandler : IRateLimiter
    {

        public async Task<HttpResponse> ExecuteAsync(Func<Task<HttpResponse>> action)
        {
            var response = await action();
            if (response.StatusCode == StatusCodes.Status429TooManyRequests)
            {
                throw new UnexpectedResponseStatusException(
                    "A rate limit has been reached.",
                    HttpStatusCode.TooManyRequests);
            }
           
            return response;
        }
    }

}
