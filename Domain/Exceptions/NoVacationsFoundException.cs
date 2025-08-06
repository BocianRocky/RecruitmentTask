namespace Domain.Exceptions;

public class NoVacationsFoundException:Exception
{
    public NoVacationsFoundException(string message): base(message)
    {
    }
}