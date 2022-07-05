using Xunit;
using TvMazeApi.Repository.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvMazeApi.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TvMazeApi.Repository.Models.Dtos;
using System.Net;

namespace TvMazeApi.Repository.Controllers.Tests
{
    public class ShowsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public ShowsControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _configuration = factory.Services.GetRequiredService<IConfiguration>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Theory]
        [InlineData("Moon Knight", 1)]
        public async Task GetShowTest_ReturnsOk(string q, int expectedShowCount)
        {
            var response = await _client.GetAsync($"/api/shows?q={q}");
            var content = await response.Content.ReadAsStringAsync();
            var shows = JsonConvert.DeserializeObject<IEnumerable<ShowDto>>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedShowCount, shows.Count());
        }

        [Theory]
        [InlineData("Not Existing Show")]
        public async Task GetShowTest_Returns404(string q)
        {
            var response = await _client.GetAsync($"/api/shows?q={q}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}