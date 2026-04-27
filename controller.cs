using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DB;
using Model;
using VModel;

namespace Controller;
public class Program
{
    public static async Task<IEnumerable<Employee>> getEmployees()
    {
        using var db = new AppDbContext();
        return await db.Employees.AsNoTracking().ToListAsync();
    }
    
    public static async Task<IList<EmployeeGroupResult>> GetEmployeeGroupByEmpIDList(){
        using var db=new AppDbContext();
        var result=await (from e in db.Employees.AsNoTracking()
                    join d in db.Departments.AsNoTracking()
                    on e.DeptId equals d.DeptId 
                    into DeptList
                    from d in DeptList.DefaultIfEmpty()
                   select new EmployeeGroupResult{
                        DeptId=e.EmpId,
                        Employees=e.FullName
                   }).ToListAsync();

        return  result;
    }
    
    // public static async Task<IEnumerable<E>>

        public static async Task Main()
        {
        var result = await GetEmployeeGroupByEmpIDList();
        Console.Write(result.Count()); 
    }
}