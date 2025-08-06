using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _context;
    
    public TeamRepository(AppDbContext context)
    {
        _context = context;
    }
    //2c
    public async Task<List<Team>> GetTeamsWithoutVacationsInYearAsync(int year)
    {
        try
        {
            return await _context.Teams.Include(t => t.Employees).ThenInclude(e => e.Vacations)
                .Where(t => !t.Employees.Any(e =>
                    e.Vacations.Any(v => v.DateSince.Year == year || v.DateUntil.Year == year))).ToListAsync();
        }catch(Exception ex)
        {
            throw new RepositoryException("An error occurred while fetching teams without vacations in the specified year.");
        }
    }
}