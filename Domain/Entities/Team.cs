namespace Domain.Entities;

public class Team
{
    public int Id { get; set;}
    public string Name { get; set;}
    
    public ICollection<Employee> Employees { get; set;}
}