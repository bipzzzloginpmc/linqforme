using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Model;
public class Employee
{
    [System.ComponentModel.DataAnnotations.Key]
    public int EmpId { get; set; }
    public Char Gender{get;set;}
     public string? FirstName
    {
        get => field;
        set => field = this.Gender switch
        {
            'M' => $"Mr {value}",
            'F' => $"Mrs {value}",
            _   => $"Ot {value}"
        };
    }
    public string? LastName { get=>field;set=>field=value; }
    public string? FullName =>$"{this.FirstName} {this.LastName}";
    public string? EmpLocation { get; set; }
    public int DeptId { get; set; }
    public int AuditId { get; set; }
    public Department? Department { get; set; }
    public Audit? Audit { get; set; }
}
public class Department
{
    [System.ComponentModel.DataAnnotations.Key]
    public int DeptId { get; set; }
    public string? DeptName { get; set; }
    public string? Location { get; set; }
     public ICollection<Employee> Employees { get; set; } = [];
}
public class Audit
{
    [System.ComponentModel.DataAnnotations.Key]
    public int AuditId { get; set; }
    public string? Role { get; set; }
    public string? Position { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public int SSFPercent { get; set; }
    public ICollection<Employee> Employees { get; set; } = [];
}

