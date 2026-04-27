using Model;
namespace VModel;
public class EmployeeGroupResult
{
    public int DeptId { get; set; }
    public IEnumerable<Employee> Employees { get; set; } = [];
}