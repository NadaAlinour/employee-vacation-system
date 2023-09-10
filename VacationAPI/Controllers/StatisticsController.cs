using Microsoft.AspNetCore.Mvc;
using VacationApplication;
using VacationDomain.Models;
using VacationApplication.Services;
using VacationPersistence;
using VacationApplication.DataService;
using Microsoft.AspNetCore.Authorization;

namespace VacationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // get all as default
        [Authorize]

        [HttpGet]
        public IActionResult GetAll()
        {
            List<VacationCount> vacationCount = _statisticsService.getAllStats();
           
            return Ok(vacationCount);
        }

        // get stats within a specific period of time
        [Authorize]

        [HttpPost]
        public IActionResult GetStatistics([FromBody] DateObject dateObject)
        {
            List<VacationCount> vacationCount = _statisticsService.getStats(dateObject);

            return Ok(vacationCount);
        }


    }
}
