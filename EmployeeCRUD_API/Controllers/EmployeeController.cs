using EmployeeCRUD_API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRUD_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeCRUD_API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeInfo _IEmployee;

        public EmployeeController(IEmployeeInfo IEmployee)
        {
            _IEmployee = IEmployee;
        }

        // GET: api/employee>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeInfo>>> Get()
        {
            return await Task.FromResult(_IEmployee.GetEmployees());
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeInfo>> Get(int id)
        {
            var employees = await Task.FromResult(_IEmployee.GetEmployeeById(id));
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        // POST api/employee
        [HttpPost]
        public async Task<ActionResult<EmployeeInfo>> Post(EmployeeInfo employee)
        {
            _IEmployee.AddEmployee(employee);
            return await Task.FromResult(employee);
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeInfo>> Put(int id, EmployeeInfo employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
            try
            {
                _IEmployee.UpdateEmployee(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(employee);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeInfo>> Delete(int id)
        {
            var employee = _IEmployee.DeleteEmployee(id);
            return await Task.FromResult(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _IEmployee.CheckEmployee(id);
        }
    }
}
