using Microsoft.EntityFrameworkCore;
using NZWalksRegions.API.Data;
using NZWalksRegions.API.Models.Domain;
using NZWalksRegions.API.Repositories;

namespace NZWalksRegions.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private NZWalksRegionDataContext dbContext;
        public RegionRepository(NZWalksRegionDataContext _dBContext)
        {
            this.dbContext = _dBContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id,Region region)
        {
            Region existingRegion = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            Region region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return null;
            }
            dbContext.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }
    }
}
