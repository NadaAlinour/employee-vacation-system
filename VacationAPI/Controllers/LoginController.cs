using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VacationApplication;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using VacationApplication.DataService;
using VacationDomain.Models;

namespace VacationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        readonly private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        public IActionResult Login([FromBody] Admin admin)
        {
            string res = _loginService.Login(admin);

            if (res == "-1")
                return new JsonResult(new { statusCode = 401, message = "Email or password is incorrect." });
                //return Unauthorized(new {message = "Wrong login information."});

            return new JsonResult(new {token = res});

        }
    }
}
