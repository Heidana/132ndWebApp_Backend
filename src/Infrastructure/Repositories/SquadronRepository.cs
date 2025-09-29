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
            return await _context.Squadrons.FindAsync(id);
        }
        public async Task<Squadron> CreateAsync(Squadron squadron)
        {
            await _context.Squadrons.AddAsync(squadron);
            await _context.SaveChangesAsync();
            return squadron;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
