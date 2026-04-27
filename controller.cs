using Microsoft.EntityFrameworkCore;
public class Program
{
    public static async Task Main()
    {
        using var db = new AppDbContext();

        // creates database + tables if they don't exist
        await db.Database.EnsureCreatedAsync();

        Console.WriteLine("Connected successfully!");
        Console.WriteLine($"Employees : {await db.Employees.CountAsync()}");
        Console.WriteLine($"Departments: {await db.Departments.CountAsync()}");
        Console.WriteLine($"Audits    : {await db.Audits.CountAsync()}");
    }
}