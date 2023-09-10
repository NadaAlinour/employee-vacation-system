using Microsoft.AspNetCore.Mvc;
using VacationApplication;
using VacationDomain.Models;
using VacationApplication.Services;
using System.Text.Json;
using VacationPersistence;
using VacationApplication.DataService;
using Microsoft.AspNetCore.Authorization;

namespace VacationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacationController : ControllerBase
    {
        private readonly VacationService _vacationService;


        public VacationController(VacationService vacationService)
        {
               _vacationService = vacationService;
        }

        [Authorize]

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Vacation> vacations = _vacationService.getAllVacations();
            return Ok(vacations);
        }

        [Authorize]

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Vacation vacation = _vacationService.getVacation(id);

            return Ok(vacation);
        }

        [Authorize]

        [HttpPost]
        public IActionResult Post([FromBody] VacationInfo vacationInfo)
        {
            Message message = _vacationService.addVacation(vacationInfo);

            if (message.Type == "error")
                return new JsonResult(new { error = message.Content });

            else return new JsonResult(new { message = message.Content });


        }

        [Authorize]


        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] VacationInfo vacationInfo)
        {
            Message message = _vacationService.updateVacation(id, vacationInfo);

            if (message.Type == "error")
                return new JsonResult(new { error = message.Content });

            else return new JsonResult(new { message = message.Content });


        }


        [Authorize]

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Message message = _vacationService.deleteVacation(id);
            return new JsonResult(new { message = message.Content });
         
        }


    }
}
