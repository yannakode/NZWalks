using NZWalks.Models.Domain;
using NZWalks.Models.Domain.DTO;

namespace NZWalks.Models.DTO
{
    public class AddWalkRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
