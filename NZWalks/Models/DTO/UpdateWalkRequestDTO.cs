using NZWalks.Models.Domain;
using NZWalks.Models.Domain.DTO;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Region Region { get; set; }
        [Required]
        public Difficulty Difficulty { get; set; }
    }
}
