using Application.Services;
using Infrastructure.Repositories;
using Xunit;

namespace Application.Tests;

public class EmployeeStructureTest
{
    [Theory]
    [InlineData(2, 1, 1)]  //row1 = 1
    [InlineData(4, 3, null)] //row2 = null
    [InlineData(4, 1, 2)]    //row3 = 2
    public void GetSuperiorRowOfEmployee_ReturnsCorrectLevel(int employeeId, int level, int? expected)
    {
        var repository = new EmployeeRepository();
        var service = new EmployeeStructureService(repository);
        
        var result = service.GetSuperiorRowOfEmployee(employeeId, level);
        
        Assert.Equal(expected, result);
    }
}