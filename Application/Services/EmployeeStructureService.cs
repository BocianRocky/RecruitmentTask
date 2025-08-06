using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;

namespace Application.Services;

public class EmployeeStructureService
{
    private readonly IEmployeeRepository _employeeRepository;
    private List<EmployeeStructure> _structure = new();

    public EmployeeStructureService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        BuildStructure();
    }

    private void BuildStructure()
    {
        var employees = _employeeRepository.GetEmployees();
        if (employees == null || !employees.Any())
        {
            throw new EmployeeNotFoundException("No employees found to build structure.");
        }
        var employeeMap = employees.ToDictionary(e => e.Id);

        foreach (var employee in employees)
        {
            int level = 1;
            var current = employee;

            while (current.SuperiorId.HasValue)
            {
                var superiorId = current.SuperiorId.Value;

                _structure.Add(
                    new EmployeeStructure
                {
                    EmployeeId = employee.Id,
                    SuperiorId = superiorId,
                    Level = level
                });

                if (!employeeMap.ContainsKey(superiorId))
                    break;

                current = employeeMap[superiorId];
                level++;
            }
        }
    }
    //1
    public int? GetSuperiorRowOfEmployee(int employeeId, int superiorId) //? is unused, because of exceptions - nvm i need for test purposes
    {
        if (!_structure.Any(s => s.EmployeeId == employeeId))
        {
            throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
        }
        if (!_structure.Any(s => s.SuperiorId == superiorId || s.EmployeeId == superiorId)) //superior must be an employee
        {
            throw new EmployeeNotFoundException($"Superior with ID {superiorId} not found.");
        }
        var relation = _structure.FirstOrDefault(s =>
            s.EmployeeId == employeeId && s.SuperiorId == superiorId);
        /*
        if (relation == null)
        {
            throw new EmployeeRelationNotFoundException($"No direct relation found between employee {employeeId} and superior {superiorId}.");
        }
        */ 
        //i can't throw exception here, because it is not required by the task, i need to return null for test purposes

        return relation?.Level;
    }
    
    
}