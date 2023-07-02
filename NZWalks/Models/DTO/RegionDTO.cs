using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.Domain.DTO
{
    public class RegionDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The code is required")]
        [StringLength(3, ErrorMessage = "The code must have only 3 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [StringLength(100, ErrorMessage = "The name must have 100 characters or fewert")]
        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
