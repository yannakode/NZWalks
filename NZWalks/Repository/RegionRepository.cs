using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.Domain.DTO;
using NZWalks.Models.DTO;
using NZWalks.Repository.Interface;

namespace NZWalks.Repository
{
    public class RegionRepository : IRegionRepository
    {
        public readonly ApplicationDbContext _context;

        public RegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Region>> ShowAllRegions()
        {
            IEnumerable<Region> regionList = await _context.regions.ToListAsync();
            return regionList;
        }

        public async Task<Region> GetRegionById(Guid id)
        {
            var region = await _context.regions.FirstOrDefaultAsync(r => r.Id == id);
            return region;
        }

        public async Task<Region> CreateRegion(Region region)
        {
            await _context.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegion(Guid id, Region regionToUpdate)
        {
            var existingRegion = await _context.regions.FirstOrDefaultAsync(r => r.Id == id);
            existingRegion.Name = regionToUpdate.Name;
            existingRegion.Code = regionToUpdate.Code;
            existingRegion.RegionImageURL = regionToUpdate.RegionImageURL;
            await _context.SaveChangesAsync();
            return existingRegion;
        }
        public async Task<bool> DeleteRegion(Guid id)
        {
            var regionToDelete = await _context.regions.FirstOrDefaultAsync(r => r.Id == id);
            _context.regions.Remove(regionToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
