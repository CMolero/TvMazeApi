namespace TvMazeApi.Repository.Controllers.Tests
{
    internal class WebApplicationFactoryClientOptions : Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
    {
        public bool AllowAutoRedirect { get; set; }
    }
}