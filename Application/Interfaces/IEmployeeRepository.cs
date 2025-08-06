using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IEmployeeRepository
{
    List<Employee> GetEmployees();
    Task<List<EmployeeDTO>> GetEmployeesWithVacationYearAsync(int year, string teamName);
    Task<List<EmployeeVacationDaysUsageDTO>> GetEmployeesWithUsedVacationDaysYearAsync();

}