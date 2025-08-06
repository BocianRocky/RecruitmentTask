using Domain.Entities;

namespace Application.Interfaces;

public interface IVacationDaysUsageService
{
    int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage);
    bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage);
}