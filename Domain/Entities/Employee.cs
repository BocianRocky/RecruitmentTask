namespace Domain.Entities;

public class Employee {
    public int Id { get; set;}
    public String Name { get; set;}
    public int? SuperiorId { get; set;}
    public virtual Employee Superior { get; set;}
    public int TeamId { get; set;}
    public int VacationPackageId { get; set;}
    
    public VacationPackage VacationPackage { get; set;}
    public Team Team { get; set;}
    public ICollection<Vacation> Vacations { get; set;}
    
}