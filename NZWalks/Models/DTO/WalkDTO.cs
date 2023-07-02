using NZWalks.Models.Domain;
using NZWalks.Models.Domain.DTO;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [StringLength(100, ErrorMessage = "The name must have 100 characters or fewer")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is required")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The length must be a positive value")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
