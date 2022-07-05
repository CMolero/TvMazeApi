using TvMazeApi.Repository.Strategy;

namespace TvMazeApi.Repository.Helpers
{
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;
        private readonly IRateLimiter _rateLimiter;

        public HttpClientHelper(HttpClient httpClient, IRateLimiter rateLimiter)
        {
            _httpClient = httpClient;
            _rateLimiter = rateLimiter;
        }

        public async Task<T> GetAsync<T>(string url, Func<T>? notFoundHandler = null)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            if (notFoundHandler != null)
            {
                return notFoundHandler();
            }
            throw new HttpRequestException($"{response.StatusCode} {response.ReasonPhrase}");
        }

        private static Func<T> DefaultNotFoundResponseHandler<T>()
        {
            var resultType = typeof(T);

            if (resultType.IsConstructedGenericType && resultType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var emptyEnumerable = typeof(Enumerable)
                    .GetMethod(nameof(Enumerable.Empty))
                    .MakeGenericMethod(typeof(T).GenericTypeArguments[0])
                    .Invoke(null, null);

                return () => (T)emptyEnumerable;
            }
            return () => default(T);
        }
    }
}
