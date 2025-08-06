using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;

namespace Application.Queries;

public class GetEmployeesWithUsedVacationDaysYearQuery
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public GetEmployeesWithUsedVacationDaysYearQuery(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task<List<EmployeeVacationDaysUsageDTO>> ExecuteAsync()
    {
        var emps = await _employeeRepository.GetEmployeesWithUsedVacationDaysYearAsync();
        
        if(emps == null || emps.Count == 0)
        {
            throw new EmployeeNotFoundException("Nie znaleziono pracowników z wykorzystanymi dniami urlopu w tym roku.");
        }
        return emps;
        
        
    }
}