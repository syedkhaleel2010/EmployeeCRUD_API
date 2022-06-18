using EmployeeCRUD_API.Models;

namespace EmployeeCRUD_API.Interface
{
    public interface IEmployeeInfo
    {
        public void AddEmployee(EmployeeInfo employee);
        public List<EmployeeInfo> GetEmployees();
        public EmployeeInfo GetEmployeeById(int id);
        public void UpdateEmployee(EmployeeInfo employee);
        public EmployeeInfo DeleteEmployee(int id);
        public bool CheckEmployee(int id);
    }
}
