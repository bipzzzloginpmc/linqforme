using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Employee>   Employees   { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Audit>      Audits      { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
         options.UseSqlServer(
        "Server=(localdb)\\MSSQLLocalDB;" +
        "Database=LinqPracticeDb;" +
        "Trusted_Connection=True;" +
        "TrustServerCertificate=True"
        );
    }
}