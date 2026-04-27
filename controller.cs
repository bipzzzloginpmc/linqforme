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
        var result=await (from m in db.Employees.AsNoTracking()
                    .Include(x=>x.Audit)
                    .Include(x=>x.Department)
                   orderby m.Gender, m.DeptId ascending
                   group m by m.Department.DeptName into Dep
                   select new EmployeeGroupResult {
                        DeptId=Dep.Key,
                        Employees=Dep.ToList()
                   }).ToListAsync();

        return result;
    }
    
    // public static async Task<IEnumerable<E>>

        public static async Task Main()
        {
        var result = await GetEmployeeGroupByEmpIDList();
        foreach(var s in result){
            Console.WriteLine(s.DeptId);
            foreach(var i in s.Employees){
            Console.WriteLine($"{i.EmpId} - {i.FullName} - {i.EmpLocation} - {i.Department?.DeptName} - {i.Audit?.NetSalary}");
            }
        }

       
    }
}