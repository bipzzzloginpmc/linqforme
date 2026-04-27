using Microsoft.EntityFrameworkCore;
using DB;
using Model;

namespace Controller;
public class Program
{
    public static async Task<IEnumerable<Employee>> getEmployees()
    {
        using var db = new AppDbContext();
        return await db.Employees.AsNoTracking().ToListAsync();
    }
    public static async Task Main()
    {
        var result = await getEmployees();

        Console.WriteLine($"Employees found: {result.Count()}");
        foreach (Employee emp in result)
        {
            Console.WriteLine($"{emp.EmpId}- {emp.FirstName}");
        }

        if (!result.Any())
        {
            Console.WriteLine("No employees were returned. Check the connection string and database name in AppDbContext.");
        }
    }
}