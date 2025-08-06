using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Application.Tests;

public class EmployeeQueryTests
{
    
    [Fact]
    public async Task GetEmployeesWithVacationYearQuery_ReturnsEmployees()
    {
        
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        
        using (var context = new AppDbContext(options))
        {
            SeedData(context);
        }
        using (var context = new AppDbContext(options))
        {
            IEmployeeRepository employeeRepo = new EmployeeRepository(context);
            var query = new GetEmployeesWithVacationYearQuery(employeeRepo);

            try
            {
                List<EmployeeDTO> employees = await query.ExecuteAsync(".NET", 2023);

                
                Assert.NotEmpty(employees);
                Assert.All(employees, e => Assert.Contains(".NET", e.Name));
            }
            catch (Exception ex)
            {
                Assert.False(true, $"Exception was thrown: {ex.Message}"); 
            }
        }
    }
    
    private void SeedData(AppDbContext context)
    {
        if (context.Employees.Any()) return;

        var teamDotNet = new Team { Name = ".NET" };
        var teamJava = new Team { Name = "Java" };
        var teamQA = new Team { Name = "QA" };
        context.Teams.AddRange(teamDotNet, teamJava, teamQA);

        var package2023 = new VacationPackage { Name = "Standard 2023", GrantedDays = 26, Year = 2023 };
        var package2024 = new VacationPackage { Name = "Standard 2024", GrantedDays = 28, Year = 2024 };
        var package2025 = new VacationPackage { Name = "Premium 2025", GrantedDays = 30, Year = 2025 };
        context.VacationPackage.AddRange(package2023, package2024, package2025);

        context.SaveChanges();

        var employees = new List<Employee>
        {
            new Employee { Name = "Jan Kowalski", TeamId = teamDotNet.Id, VacationPackageId = package2023.Id },
            new Employee { Name = "Anna Nowak", TeamId = teamDotNet.Id, VacationPackageId = package2024.Id },
            new Employee { Name = "Tomasz Zielinski", TeamId = teamDotNet.Id, VacationPackageId = package2025.Id },

            new Employee { Name = "Katarzyna Wisniewska", TeamId = teamJava.Id, VacationPackageId = package2023.Id },
            new Employee { Name = "Piotr Maj", TeamId = teamJava.Id, VacationPackageId = package2024.Id }, 
            new Employee { Name = "Ewa Dabrowska", TeamId = teamJava.Id, VacationPackageId = package2025.Id },

            new Employee { Name = "Marek Wojcik", TeamId = teamQA.Id, VacationPackageId = package2023.Id },
            new Employee { Name = "Barbara Lewandowska", TeamId = teamQA.Id, VacationPackageId = package2024.Id },
            new Employee { Name = "Zbigniew Adamczyk", TeamId = teamQA.Id, VacationPackageId = package2025.Id }
        };

        context.Employees.AddRange(employees);
        context.SaveChanges();

        var vacations = new List<Vacation>
        {
            new Vacation
            {
                EmployeeId = employees[0].Id,
                DateSince = new DateTime(2023, 7, 1),
                DateUntil = new DateTime(2023, 7, 10),
                NumberOfHours = 80,
                IsPartialVacation = 0
            },
            
        };

        context.Vacations.AddRange(vacations);
        context.SaveChanges();
    }
    
}
    
