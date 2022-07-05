using AutoMapper;
using TvMazeApi.Repository.Models;
using TvMazeApi.Repository.Models.Dtos;

namespace TvMazeApi.Repository.Maps
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Show, ShowDto>();
            CreateMap<Episode, EpisodeDto>();
        }
    }
}
