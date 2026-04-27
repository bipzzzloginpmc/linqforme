using Microsoft.EntityFrameworkCore;
using Model;

namespace DB;
public class AppDbContext : DbContext
{
    public DbSet<Employee>   Employees   { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Audit>      Audits      { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
         options.UseSqlServer(
        "Server=(localdb)\\MSSQLLocalDB;" +
        "Database=OnlineStore;" +
        "Trusted_Connection=True;" +
        "TrustServerCertificate=True"
        );
    }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "dbo");
                entity.HasKey(e => e.EmpId);
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Employees)
                      .HasForeignKey(e => e.DeptId);
                entity.HasOne(e => e.Audit)
                      .WithMany(a => a.Employees)
                      .HasForeignKey(e => e.AuditId);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department", "dbo");
                entity.HasKey(d => d.DeptId);
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("Audit", "dbo");
                entity.HasKey(a => a.AuditId);
            });
        }
}