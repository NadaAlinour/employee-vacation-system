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
    public class UnitController : ControllerBase
    {
        private readonly UnitService _unitService;

        public UnitController(UnitService unitService)
        {
            _unitService = unitService;
        }

        [Authorize]

        [HttpGet]

        public IActionResult GetAll()
        {
            IEnumerable<Unit> units = _unitService.getAllUnits();
            return Ok(units);
        }


        [Authorize]

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Unit unit = _unitService.getUnit(id);
            return Ok(unit);
        }
    }
}
