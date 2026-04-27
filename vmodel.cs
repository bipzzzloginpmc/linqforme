using Model;
namespace VModel;
public class EmployeeGroupResult
{
    public String DeptId { get; set; }
    public IEnumerable<Employee> Employees { get; set; } = [];
}