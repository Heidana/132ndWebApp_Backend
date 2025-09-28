using _132ndWebsite.Core.Models;

namespace _132ndWebsite.Application.Interfaces;

public interface ISquadronService
{
    Task<IEnumerable<Squadron>> GetAllSquadronsAsync();
    Task<Squadron?> GetSquadronByIdAsync(int id);
}