using Application.Services;
using Domain.Entities;
using Xunit;

namespace Application.Tests;

public class EmployeeRequestVacationTest
{
    [Fact]
    public void employee_can_request_vacation()
    {
        // Arrange
        var service = new VacationDaysUsageService();
        
        var employee = new Employee { Id = 1, Name = "Adam" };

        var vacations = new List<Vacation>
        {
            new Vacation
            {
                EmployeeId = 1,
                DateSince = new DateTime(DateTime.Today.Year, 7, 1),
                DateUntil = new DateTime(DateTime.Today.Year, 7, 5),
                IsPartialVacation = 0
            },
            new Vacation 
            {
                EmployeeId = 1,
                DateSince = new DateTime(DateTime.Today.Year, 6, 1),
                DateUntil = new DateTime(DateTime.Today.Year, 6, 5),
                IsPartialVacation = 0
            }
        };
        var package = new VacationPackage { GrantedDays = 25 };
        
        // Act
        
        
        var how = service.CountFreeDaysForEmployee(employee, vacations, package);
        var result = service.IfEmployeeCanRequestVacation(employee, vacations, package);
        
        
        // Assert
        
        Assert.Equal(15, how);
        Assert.True(result); 
        
    }
    
    [Fact]
    public void employee_cant_request_vacation()
    {
        // Arrange
        var service = new VacationDaysUsageService();
        var employee = new Employee { Id = 1, Name = "Adam" };
        var vacations = new List<Vacation>
        {
            new Vacation
            {
                EmployeeId = 1,
                DateSince = new DateTime(DateTime.Today.Year, 7, 1),
                DateUntil = new DateTime(DateTime.Today.Year, 7, 5),
                IsPartialVacation = 0
            },
            new Vacation
            {
                EmployeeId = 1,
                DateSince = new DateTime(DateTime.Today.Year, 6, 1),
                DateUntil = new DateTime(DateTime.Today.Year, 6, 3),
                IsPartialVacation = 0
            }
        };
        var package = new VacationPackage { GrantedDays = 8 };
        
        
        
        // Act
        var how = service.CountFreeDaysForEmployee(employee, vacations, package);
        var result = service.IfEmployeeCanRequestVacation(employee, vacations, package);
        
        
        
        
        // Assert
        Assert.Equal(0, how);
        Assert.False(result); //5 from 5 already used
    }
    
}