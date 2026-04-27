using Model;
namespace VModel;
public class EmployeeGroupResult
{
    public string DeptId { get; set; }
    public IEnumerable<Employee> Employees { get; set; } = [];
}