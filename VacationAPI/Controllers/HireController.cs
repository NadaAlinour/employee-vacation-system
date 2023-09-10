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

    public class HireController : ControllerBase
    {
        private readonly HireService _hireService;

        public HireController(HireService hireService)
        {
            _hireService = hireService;
        }


        [Authorize]

        [HttpGet("{vid}")]
        public IActionResult GetAll(int vid) {

            List<Hire> hireList = _hireService.getHireRules(vid);

            return Ok(hireList);
        
        }

    }
}
