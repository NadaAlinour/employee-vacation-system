using Microsoft.AspNetCore.Mvc;
using VacationApplication;
using VacationDomain.Models;
using VacationApplication.Services;
using VacationApplication.DataService;
using Microsoft.AspNetCore.Authorization;

namespace VacationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
        }


        [Authorize]

        [HttpPost]
        public IActionResult AddRequest([FromBody] EmployeeVacation employeeVacation)
        { 
           Message message = _requestService.AddRequest(employeeVacation);
            
           if (message.Type == "error")
            {
                return new JsonResult(new {errorMessage = message.Content});
            }

           // success message
           else
            {
                return new JsonResult(new { message = message.Content });
            }

        }


    }
}
