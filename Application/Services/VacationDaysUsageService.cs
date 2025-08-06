using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class VacationDaysUsageService : IVacationDaysUsageService
{
    public VacationDaysUsageService() { }
    
    //3
    public int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations,
        VacationPackage vacationPackage )
    {
        if (employee == null)
        {
            throw new EmployeeNotFoundException("Employee not found.");
        }
        if (vacationPackage == null)
        {
            throw new VacationPackageNotFoundException("Vacation package not found.");
        }
        if(vacations==null || !vacations.Any())
        {
            throw new NoVacationsFoundException("No vacations found for the employee.");
        }
        var today = DateTime.Today;
        var totalDays = vacationPackage.GrantedDays;
            
        var usedDays = vacations.Where(v=> v.EmployeeId == employee.Id)
            .Where(v => v.DateSince.Year == today.Year || v.DateUntil < today && v.IsPartialVacation==0)
            .Sum(v => (v.DateUntil - v.DateSince).Days + 1);
        
        return totalDays - usedDays;
    }
    public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
    {
        //i needn't validation here, because it is done in CountFreeDaysForEmployee
        int remainingDays = CountFreeDaysForEmployee(employee, vacations, vacationPackage);
        
        return remainingDays > 0;
    }

    
}