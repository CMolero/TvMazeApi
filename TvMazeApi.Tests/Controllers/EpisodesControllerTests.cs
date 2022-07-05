using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TvMazeApi.Tests;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TvMazeApi.Repository.Models.Dtos;
using System.Net;
using TvMazeApi.Repository.Models;

namespace TvMazeApi.Repository.Controllers.Tests
{
    public class EpisodesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public EpisodesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _configuration = factory.Services.GetRequiredService<IConfiguration>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }


        [Theory]
        [InlineData(1)]
        public async Task  GetEpisodesForShowTest_Returns404(int showId)
        {

            var response = await _client.GetAsync($"api/shows/{showId}/episodes");
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("Game of Thrones", 73)]
        public async Task GetEpisodesForShowTest_ReturnsOkWithEpisodes(string q, int expectedEpisodeCount)
        {
            var responseShow = await _client.GetAsync($"/api/shows?q={q}");
            var contentShow = await responseShow.Content.ReadAsStringAsync();
            var show =  JsonConvert.DeserializeObject<IEnumerable<ShowDto>>(contentShow);
            
            var response = await _client.GetAsync($"api/shows/{show.First().Id}/episodes");
            var content = await response.Content.ReadAsStringAsync();
            var episodes = JsonConvert.DeserializeObject<IEnumerable<EpisodeDto>>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedEpisodeCount, episodes.Count());
        }

    }
}