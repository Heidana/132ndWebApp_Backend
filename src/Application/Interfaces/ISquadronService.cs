using _132ndWebsite.Application.Dtos;
using _132ndWebsite.Core.Models;

namespace _132ndWebsite.Application.Interfaces;

public interface ISquadronService
{
    Task<IEnumerable<Squadron>> GetAllSquadronsAsync();
    Task<Squadron?> GetSquadronByIdAsync(int id);
    Task<Squadron> CreateSquadronAsync(CreateSquadronDto squadronDto);
    Task<Squadron?> UpdateSquadronAsync(int id, UpdateSquadronDto squadronDto);
}