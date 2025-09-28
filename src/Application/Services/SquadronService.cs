using _132ndWebsite.Application.Dtos;
using _132ndWebsite.Application.Interfaces;
using _132ndWebsite.Core.Models;
using _132ndWebsite.Infrastructure.Repositories;

namespace _132ndWebsite.Application.Services;

public class SquadronService : ISquadronService
{
    private readonly ISquadronRepository _squadronRepository;

    public SquadronService(ISquadronRepository squadronRepository)
    {
        _squadronRepository = squadronRepository;
    }

    public async Task<IEnumerable<Squadron>> GetAllSquadronsAsync()
    {
        // Future business logic can go here (e.g., caching, validation, mapping to DTOs)
        return await _squadronRepository.GetAllAsync();
    }

    public async Task<Squadron?> GetSquadronByIdAsync(int id)
    {
        // Future business logic can go here
        return await _squadronRepository.GetByIdAsync(id);
    }
    public async Task<Squadron> CreateSquadronAsync(CreateSquadronDto squadronDto)
        {
            var newSquadron = new Squadron(0, squadronDto.Name, squadronDto.Callsign);
            return await _squadronRepository.CreateAsync(newSquadron);
        }
}