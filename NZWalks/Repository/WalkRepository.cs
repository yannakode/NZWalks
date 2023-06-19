using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Repository.Interface;

namespace NZWalks.Repository
{
    public class WalkRepository : IWalkRepository
    {
        public readonly ApplicationDbContext _context;

        public WalkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Walk?>> ShowAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, 
            bool isAcending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _context.walks.Include("Difficulty").Include("Region").AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == null)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));
                }
            }

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAcending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAcending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            var skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }
        public async Task<Walk?> GetWalkById(Guid id)
        {
            var walk = await _context.walks.FirstOrDefaultAsync(w => w.Id == id);
            return walk;
        }
        public async Task<Walk?> CreateWalk(Walk? walk)
        {
            await _context.walks.AddAsync(walk);
            _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateWalk(Guid id, Walk? walk)
        {
            var walkToUpdate = await _context.walks.FirstOrDefaultAsync(w => w.Id == id);
            walkToUpdate.Name = walk.Name;
            walkToUpdate.Description = walk.Description;
            walkToUpdate.LengthInKm = walk.LengthInKm;
            walkToUpdate.WalkImageUrl = walk.WalkImageUrl;
            walkToUpdate.DifficultyId = walk.DifficultyId;
            walkToUpdate.RegionId = walk.RegionId;
            await _context.SaveChangesAsync();
            return walkToUpdate;
        }
        public async Task<bool> DeleteWalk(Guid id)
        {
            var walkToDelete = await _context.walks.FirstOrDefaultAsync(w => w.Id == id);
            _context.walks.Remove(walkToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
