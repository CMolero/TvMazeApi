using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TvMazeApi.Repository.Models.Dtos;
using TvMazeApi.Repository.Repositories;
using TvMazeApi.Repository.Services;

namespace TvMazeApi.Repository.Controllers
{
    [Route("api/shows")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IScraperService _scraperService;
        private readonly IShowRepository _showRepository;
        private readonly IMapper _mapper;

        public ShowsController(IScraperService scraperService, IShowRepository showRepository, IMapper mapper)
        {
            _scraperService = scraperService ?? throw new ArgumentNullException(nameof(scraperService));
            _showRepository = showRepository ?? throw new ArgumentNullException(nameof(showRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// API endpoint to get shows.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ShowDto>>> GetShow([FromQuery]string q)
        {

            await _scraperService.Scrape(q);
            var shows = await _showRepository.GetShows(q);
            if (shows.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ShowDto>>(shows));
        }
    }
}
