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
    public class RuleController : ControllerBase
    {
        private readonly RuleService _ruleService;

        public RuleController(RuleService ruleService)
        {
            _ruleService = ruleService;
        }


        [Authorize]

        [HttpGet("{vid}")]
      public IActionResult Get(int vid) {

            Rule rule = _ruleService.GetRule(vid);
            return Ok(rule);
        
        }


    }
}
