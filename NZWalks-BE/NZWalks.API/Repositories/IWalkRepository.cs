using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {

        public Task<Walk> CreateAsync(Walk walk);

        public Task<Walk?> UpdateAsync(Guid id, Walk walk);

        public Task<Walk?> DeleteAsync(Guid id);

        public Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber=1, int pageSize=1000);

        public Task<Walk?> GetByIdAsync(Guid id);
    }
}
