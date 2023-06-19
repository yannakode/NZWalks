using NZWalks.Models.Domain;

namespace NZWalks.Repository.Interface
{
    public interface IWalkRepository
    {
        public Task<IEnumerable<Walk?>> ShowAllWalks();
        public Task<Walk?> GetWalkById(Guid id);
        public Task<Walk?> CreateWalk(Walk? walk);
        public Task<Walk?> UpdateWalk(Guid id, Walk? walk);
        public Task<bool> DeleteWalk(Guid id);
    }
}
