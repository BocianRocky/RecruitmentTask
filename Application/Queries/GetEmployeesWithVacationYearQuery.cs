using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Queries;

public class GetEmployeesWithVacationYearQuery
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public GetEmployeesWithVacationYearQuery(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task<List<EmployeeDTO>> ExecuteAsync(string teamName, int year)
    {
        if (string.IsNullOrWhiteSpace(teamName))
        {
            throw new TeamNotFoundException($"Team with name '{teamName}' not found.");
        }
        var employees = await _employeeRepository.GetEmployeesWithVacationYearAsync(year, teamName);
        
        if (employees == null || !employees.Any())
        {
            throw new EmployeeNotFoundException($"No employees found for team '{teamName}' in year {year}.");
        }

        return employees;

    }
    
}