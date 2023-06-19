using NZWalks.Models.Domain.DTO;

namespace NZWalks.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //navigation properties
        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
