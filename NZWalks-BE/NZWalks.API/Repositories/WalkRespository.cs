using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRespository : IWalkRepository
    {

        private NZWalksDBContext dbContext;

        public WalkRespository(NZWalksDBContext nZWalksDBContext)
        {
            dbContext = nZWalksDBContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walk.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return (await dbContext.Walk.Include("Difficulty").FirstOrDefaultAsync(w => w.Id == walk.Id) ?? null);
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn,string? filterQuery, string? sortBy, bool isAscending=true, int pageNumber=1, int pageSize=1000)
        {
            IQueryable<Walk> walk = dbContext.Walk.Include("Difficulty");

            //soring based on the parameters
            if(sortBy!=null &&sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {

                    walk = isAscending ? walk.OrderBy(w => w.Name) : walk.OrderByDescending(w => w.Name);
          
            }
            else if(sortBy != null && sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
            {
                walk = isAscending ? walk.OrderBy(w => w.LengthInKm) : walk.OrderByDescending(w => w.LengthInKm);

            }


            //filtering based on the parameters.
            if(filterOn!=null && filterQuery != null)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk= walk.Where(w => w.Name.Contains(filterQuery));
                }
            }

            //pagination based on parameter

            walk = walk.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await walk.ToListAsync();
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            dbContext.Walk.Update(walk);
            await dbContext.SaveChangesAsync();
            return await dbContext.Walk.Include("Difficulty").FirstOrDefaultAsync(w => w.Id == walk.Id);
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? existingWalk = await dbContext.Walk.FirstOrDefaultAsync(w => w.Id == id);
            if (existingWalk != null)
            {
                dbContext.Walk.Remove(existingWalk);
                await dbContext.SaveChangesAsync();

            }
            return existingWalk;
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walk.Include("Difficulty").FirstOrDefaultAsync(w => w.Id == id) ?? null;
        }
    }
}
