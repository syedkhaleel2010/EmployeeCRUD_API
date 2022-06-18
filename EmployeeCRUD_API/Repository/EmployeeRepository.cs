using EmployeeCRUD_API.Interface;
using EmployeeCRUD_API.Models;

namespace EmployeeCRUD_API.Repository
{
    public class EmployeeRepository : IEmployeeInfo
    {
        readonly DatabaseContext _dbContext = new();

        public EmployeeRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddEmployee(EmployeeInfo employee)
        {
            try
            {
                _dbContext.EmployeeInfo.Add(employee);
                _dbContext.SaveChanges();
            }
            catch 
            {

                throw;
            }
        }

        public EmployeeInfo DeleteEmployee(int id)
        {
            try
            {
                EmployeeInfo? employee = _dbContext.EmployeeInfo.Find(id);
                if (employee is null)
                    throw new ArgumentNullException();
                else
                {
                    _dbContext.EmployeeInfo.Remove(employee);
                    _dbContext.SaveChanges();
                    return employee;
                }
            }
            catch 
            {

                throw;
            }
        }

        public EmployeeInfo GetEmployeeById(int id)
        {
            try
            {
                var empInfo = _dbContext.EmployeeInfo.Where(x=>x.EmployeeId ==id).FirstOrDefault();
                if (empInfo is null)
                {
                    throw new ArgumentNullException();
                }
                else
                    return empInfo;
            }
            catch
            {

                throw;
            }
        }

        public List<EmployeeInfo> GetEmployees()
        {
            try
            {
                return _dbContext.EmployeeInfo.ToList();
            }
            catch
            {
                throw;
            }
           
        }

        public void UpdateEmployee(EmployeeInfo employee)
        {
            try
            {
                _dbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch 
            {

                throw;
            }
        }
        public bool CheckEmployee(int id)
        {
            return _dbContext.EmployeeInfo.Any(e => e.EmployeeId == id);
        }
    }
}
