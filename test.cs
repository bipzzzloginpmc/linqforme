using System.ComponentModel;

public class Employee
{
    public int EmpId { get; set; }
    public Char Gender{get;set;}
    public string? FirstName { get=>field; set=>field=this.Gender=="M"? $"Mr {value}": this.Gender=="F"? $"Mrs {value}": $"Ot {value}"; }
    public string? LastName { get=>field;set=>field=value; }
    public string? FullName =>$"{this.FirstName} {this.LastName}";
}
public class Department
{
    public int MyProperty { get; set; }
    public int MyProperty { get; set; }
    public int MyProperty { get; set; }
}
public class Address
{
    
}