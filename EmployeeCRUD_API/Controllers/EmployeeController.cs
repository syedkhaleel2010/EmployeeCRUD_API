using EmployeeCRUD_API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRUD_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
namespace EmployeeCRUD_API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize]
   //Author : Syed Khaleel,Created Date: 18 June 2022, Description : To perform CRUD operations
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeInfo _IEmployee;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeInfo IEmployee, ILogger<EmployeeController> logger, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _IEmployee = IEmployee;
            _logger = logger;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/employee>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeInfo>>> Get()
        {
            try
            {
                return await Task.FromResult(_IEmployee.GetEmployees());
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"URL : api/employee :: Error Message :{ex.Message}");

                throw;
            }
            
        }

        // GET api/employee/102
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeInfo>> Get(int id)
        {
            try
            {
                var employees = await Task.FromResult(_IEmployee.GetEmployeeById(id));
                if (employees == null)
                {
                    _logger.LogInformation(String.Format("The employee id passed as argument to API is : {0}", id));

                    return NotFound();
                }
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"URL : api/employee :: Error Message :{ex.Message}");

                return NotFound();
            }
           
        }

        // POST api/employee
        [HttpPost]
        public async Task<ActionResult<EmployeeInfo>> Post(EmployeeInfo employee)
        {
            try
            {
                _IEmployee.AddEmployee(employee);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"URL :api/employee,verb:post ::  Error Message : {ex.Message} and employee details passed to api as :{JsonSerializer.Serialize(employee)}");
                throw;
            }
            
            return await Task.FromResult(employee);
        }

        // PUT api/employee/102
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeInfo>> Put(int id, EmployeeInfo employee)
        {
            if (id != employee.EmployeeId)
            {
                _logger.LogInformation(String.Format("The employee id passed as argument to API is : {0}", id));
                return BadRequest();
            }
            try
            {
                _IEmployee.UpdateEmployee(employee);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EmployeeExists(id))
                {
                    _logger.LogInformation(String.Format($"URL: api / employee, verb: put::Error Message: { ex.Message}  The employee id passed as argument to API is : {0}", id));
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation(String.Format($"URL: api / employee, verb: put::Error Message: {ex.Message}  The employee id passed as argument to API is : {0}", id));
                    throw;
                }
            }
            return await Task.FromResult(employee);
        }
        // DELETE api/employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeInfo>> Delete(int id)
        {
            try
            {
                var employee = _IEmployee.DeleteEmployee(id);
                return await Task.FromResult(employee);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(String.Format($"URL: api / employee, verb: put::Error Message: {ex.Message} and The employee id passed as argument to API is : {0}", id));
                
                throw;
            }
        }
        private bool EmployeeExists(int id)
        {
            return _IEmployee.CheckEmployee(id);
        }
    }
}