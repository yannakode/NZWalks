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

        public async Task<IEnumerable<Walk?>> ShowAll()
        {
            IEnumerable<Walk> walkList = await _context.walks.ToListAsync();
            return walkList;
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
