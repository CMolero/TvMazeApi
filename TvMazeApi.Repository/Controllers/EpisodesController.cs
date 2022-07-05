using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TvMazeApi.Repository.Models.Dtos;
using TvMazeApi.Repository.Repositories;

namespace TvMazeApi.Repository.Controllers
{
    [Route("api/shows/{showId}/episodes")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;

        public EpisodesController(IEpisodeRepository episodeRepository, IMapper mapper)
        {
            _episodeRepository = episodeRepository ?? throw new ArgumentNullException(nameof(episodeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// Get all episodes for a show
        /// </summary>
        /// <param name="showId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EpisodeDto>>> GetEpisodesForShow(int showId)
        {
            var episodesForShow = await _episodeRepository.GetEpisodes(showId);
            if (episodesForShow.Count() == 0 || showId <= 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<EpisodeDto>>(episodesForShow));
        }
    }
}
