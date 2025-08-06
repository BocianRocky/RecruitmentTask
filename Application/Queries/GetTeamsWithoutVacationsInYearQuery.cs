using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Queries;

public class GetTeamsWithoutVacationsInYearQuery
{
    private readonly ITeamRepository _teamRepository;
    
    public GetTeamsWithoutVacationsInYearQuery(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<List<Team>> ExecuteAsync(int year)
    {
        var teams = await _teamRepository.GetTeamsWithoutVacationsInYearAsync(year);
        if (teams == null || !teams.Any())
        {
            throw new TeamNotFoundException("No teams found without vacations in the specified year.");
        }
        return teams;
    }
}