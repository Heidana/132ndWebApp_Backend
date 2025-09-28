using _132ndWebsite.Core.Models;

namespace _132ndWebsite.Infrastructure.Repositories
{
    public interface ISquadronRepository
    {
        Task<IEnumerable<Squadron>> GetAllAsync();
        Task<Squadron?> GetByIdAsync(int id);
    }
}