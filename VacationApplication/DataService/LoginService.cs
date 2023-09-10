using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VacationApplication.Repositories;
using VacationDomain.Models;

namespace VacationApplication.DataService
{
    public class LoginService
    {
        // get user data from database later
        readonly private IUserRepository<Admin> _userRepository;

        public LoginService(IUserRepository<Admin> userRepository)
        {
            _userRepository = userRepository;
        }
        public string Login(Admin admin)
        {
            /*if (userDTO.Username != "testusername" || userDTO.Password != "testpassword")
                return "-1";*/

            Admin dbAdmin = _userRepository.Get();

            if (dbAdmin.Email != admin.Email || dbAdmin.Password != admin.Password)
            {
                return "-1";
            }

            // create token
            const string secretKey = "Shared secret key";

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var jwt = new JwtSecurityToken(
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
