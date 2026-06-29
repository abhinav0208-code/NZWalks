using NZWalksRegions.API.Models.Domain;

namespace NZWalksRegions.API.Repositories
{
    public interface IRegionRepository
    {

        public Task<List<Region>> GetAllAsync();

        public Task<Region?> GetById(Guid id);

        public Task<Region> CreateAsync(Region region);

        public Task<Region> UpdateAsync(Guid id,Region region);

        public Task<Region?> DeleteAsync(Guid id);
    }
}
