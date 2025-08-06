namespace Domain.Exceptions;

public class EmployeeRelationNotFoundException:Exception
{
    public EmployeeRelationNotFoundException(string message): base(message)
    {
    }
}