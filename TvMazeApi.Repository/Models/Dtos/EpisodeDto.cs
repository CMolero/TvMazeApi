using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeApi.Repository.Models.Dtos
{
    public class EpisodeDto
    {
        
        public int Id { get; set; }
        public int Season { get; set; }
        public int? Number { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }
        public int ShowId { get; set; }

    }
}
