using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    
    public EmployeeRepository()
    {
        //for testing purposes
    }
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    public List<Employee> GetEmployees()
    {
        return new List<Employee>
        {
            new Employee { Id = 1, Name = "Jan Kowalski", SuperiorId = null },
            new Employee { Id = 2, Name = "Kamil Nowak", SuperiorId = 1 },
            new Employee { Id = 3, Name = "Anna Mariacka", SuperiorId = 1 },
            new Employee { Id = 4, Name = "Andrzej Abacki", SuperiorId = 2 },
        };
    }
    //2a
    public async Task<List<EmployeeDTO>>GetEmployeesWithVacationYearAsync(int year, string teamName)
    {
        try
        {

            return await _context.Employees.Include(e => e.Vacations)
                .Include(e => e.Team)
                .Where(e => e.Team.Name == teamName &&
                            e.Vacations.Any(v => v.DateSince.Year == year || v.DateUntil.Year == year))
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    SuperiorId = e.SuperiorId,
                }).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new RepositoryException("An error occurred while fetching employees with vacation data.");
        }
    }
    //2b
    public async Task<List<EmployeeVacationDaysUsageDTO>> GetEmployeesWithUsedVacationDaysYearAsync()
    {
        var today = DateTime.Today;

        try
        {

            var empsDto = await _context.Employees
                .Include(e => e.Vacations)
                .Select(e => new EmployeeVacationDaysUsageDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    VacationDaysUsage = e.Vacations
                        .Where(v => v.DateSince.Year == today.Year || v.DateUntil < today && v.IsPartialVacation == 0)
                        .Sum(v => (v.DateUntil - v.DateSince).Days + 1)
                })
                .Where(e => e.VacationDaysUsage > 0)
                .ToListAsync();
            return empsDto;
        }
        catch (Exception ex)
        {
            throw new RepositoryException("An error occurred while fetching employees with used vacation days.");
        }
    }

    
}