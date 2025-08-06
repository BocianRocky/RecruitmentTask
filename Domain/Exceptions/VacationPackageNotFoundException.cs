namespace Domain.Exceptions;

public class VacationPackageNotFoundException:Exception
{
    public VacationPackageNotFoundException(string message): base(message)
    {
    }
}
