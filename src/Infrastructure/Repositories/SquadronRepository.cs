using _132ndWebsite.Core.Models;
using _132ndWebsite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace _132ndWebsite.Infrastructure.Repositories
{
    public class SquadronRepository : ISquadronRepository
    {
        private readonly ApplicationDbContext _context;

        public SquadronRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Squadron>> GetAllAsync()
        {
            return await _context.Squadrons.ToListAsync();
        }

        public async Task<Squadron?> GetByIdAsync(int id)
        {
            // FindAsync is highly efficient for lookups by primary key
            return await _context.Squadrons.FindAsync(id);
        }
    }
}
