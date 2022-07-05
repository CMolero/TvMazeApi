using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeApi.Repository.Models
{
    public class Episode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public int Season { get; set; }
        [Required]
        public int Number { get; set; }
        public string? Name { get; set; }
        public string? Summary { get; set; }
        
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
        public int ShowId { get; set; }
    }
}
