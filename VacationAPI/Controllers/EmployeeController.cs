using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacationApplication;
using VacationApplication.DataService;
using VacationDomain.Models;

namespace VacationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize]

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Employee> employees = _employeeService.GetAllEmployees();

            return Ok(employees);
        }

        [Authorize]

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee employee = _employeeService.GetEmployee(id);

            if (employee == null)
            {
                return new JsonResult(new { message = "This employee does not exist." });
            }

            return Ok(employee);
        }


        [Authorize]

        [HttpPost]
        public IActionResult Post(IFormFile? file, [FromForm] string Name, [FromForm] string Phone, [FromForm] string Email, [FromForm] decimal Salary, [FromForm] DateTime HiredDate, [FromForm] Boolean IsVacationAllowed)
        {
            // "file" has to be the same name of the file expected from the frontend or request in general

            if (file != null)
            {
                var stream = file.OpenReadStream();
                _employeeService.AddEmployee(stream, Name, Phone, Email, Salary, HiredDate, IsVacationAllowed);
                return new JsonResult(new { message = "Employee has been successfully added." });

            }

            else return BadRequest();

        }


        [Authorize]

        [HttpPut("{id}")]

        public IActionResult Put(int id, IFormFile? file, [FromForm] string Name, [FromForm] string Phone, [FromForm] string Email, [FromForm] decimal Salary, [FromForm] Boolean IsVacationAllowed)
        {

            var stream = file.OpenReadStream();
            int res = _employeeService.UpdateEmployee(id, stream, Name, Phone, Email, Salary, IsVacationAllowed);


            if (res == 1)
                return new JsonResult(new { message = "Employee has been successfully updated." });

            else return BadRequest();

        }


        [Authorize]

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // call employeeService
            int res = _employeeService.DeleteEmployee(id);

            if (res == 1)
            {
                return new JsonResult(new { message = "Employee successfully deleted." });
            }
            else
            {
                return new JsonResult(new { message = "Employee does not exist." });

            }

        }

       

    }
}
