using Microsoft.AspNetCore.Mvc;
using VacationApplication;
using VacationDomain.Models;
using VacationApplication.Services;
using VacationPersistence;
using VacationPersistence.DatabaseAccess;
using VacationApplication.DataService;
using Microsoft.AspNetCore.Authorization;

namespace VacationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;
        private readonly IDataRepository<Employee> _employeeRepository;


        public SearchController(SearchService searchService, IDataRepository<Employee> employeeRepository)
        {
            _searchService = searchService;
            _employeeRepository = employeeRepository;
        }



        [Authorize]

        [HttpGet("{id}")]
        public IActionResult SearchByVacation(int id)
        {
            List<Employee> employees = _searchService.searchByVacation(id);
            return Ok(employees);
        }


        [Authorize]

        [HttpPost("{id}")]
        public IActionResult SearchByEmployee(int id, [FromBody]DateObject dateObject)
        {
            Employee employee = _employeeRepository.Get(id);
            if (employee == null) {
                return new JsonResult(new { message = "This employee does not exist." });
            }

            List<Vacation> vacations = _searchService.searchByEmployee(id, dateObject);
            return Ok(vacations);

            

        }
    }
}
