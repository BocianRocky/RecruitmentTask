using Domain.Entities;

namespace Application.Interfaces;

public interface ITeamRepository
{
    Task<List<Team>> GetTeamsWithoutVacationsInYearAsync(int year);
}