using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Employee> Employees { get; set;}
    public DbSet<Vacation> Vacations { get; set;}
    public DbSet<VacationPackage> VacationPackage { get; set;}
    public DbSet<Team> Teams { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Employee>().HasOne(e=>e.Team).WithMany(t => t.Employees).HasForeignKey(e => e.TeamId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Employee>().HasOne(e=>e.VacationPackage).WithMany(v=>v.Employees).HasForeignKey(e => e.VacationPackageId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Vacation>().HasOne(v=>v.Employee).WithMany(e=>e.Vacations).HasForeignKey(v=>v.EmployeeId).OnDelete(DeleteBehavior.Cascade);
    }
}