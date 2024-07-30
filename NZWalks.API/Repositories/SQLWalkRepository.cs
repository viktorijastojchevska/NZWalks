using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWaksDbContext _context;

        public SQLWalkRepository(NZWaksDbContext context)
        {
            _context = context;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if ( existingWalk == null)
            {
                return null;
            }
            
            _context.Walks.Remove(existingWalk);
            await _context.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await _context.Walks
                                 .Include(w => w.Region)
                                 .Include(w =>w.Difficulty)
                                 .ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var walk =  await _context.Walks
                                      .Include(w => w.Region)
                                      .Include(w => w.Difficulty)
                                      .FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await _context.Walks
                                             .Include(w => w.Region)
                                             .Include(w => w.Difficulty)
                                             .FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageURL = walk.WalkImageURL;
            existingWalk.DifficultyID = walk.DifficultyID;
            existingWalk.RegionID = walk.RegionID;

            await _context.SaveChangesAsync();
            return existingWalk;
        }
    }
}
