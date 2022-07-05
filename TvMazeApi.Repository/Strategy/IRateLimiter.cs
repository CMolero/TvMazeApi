using System.Net.Http;

namespace TvMazeApi.Repository.Strategy
{
    /// <summary>
    /// Implements rate limit defined by TvMaze
    /// </summary>
    public interface IRateLimiter
    {
        Task<HttpResponse> ExecuteAsync(Func<Task<HttpResponse>> action);
    }
}
