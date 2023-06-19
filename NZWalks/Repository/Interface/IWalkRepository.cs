using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repository.Interface
{
    public interface IWalkRepository
    {
        public Task<IEnumerable<Walk?>> ShowAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAcending = true,
            int pageNumber = 1, int pageSize = 1000);
        public Task<Walk?> GetWalkById(Guid id);
        public Task<Walk?> CreateWalk(Walk? walk);
        public Task<Walk?> UpdateWalk(Guid id, Walk? walk);
        public Task<bool> DeleteWalk(Guid id);
    }
}
