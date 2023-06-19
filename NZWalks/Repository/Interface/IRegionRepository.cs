using NZWalks.Models.Domain;
using NZWalks.Models.Domain.DTO;
using NZWalks.Models.DTO;

namespace NZWalks.Repository.Interface
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region?>> ShowAllRegions();
        Task<Region?> GetRegionById(Guid id);
        Task<Region?> CreateRegion(Region regionToCreate);
        Task<Region?> UpdateRegion(Guid id, Region regionToUpdate);
        Task<bool> DeleteRegion(Guid id);
    }
}
