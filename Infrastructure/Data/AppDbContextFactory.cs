using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(
            @"Server=localhost\SQLEXPRESS;Database=RecruitmentTaskDb;Trusted_Connection=True;TrustServerCertificate=True;",
            sqlOptions => sqlOptions.EnableRetryOnFailure()
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}