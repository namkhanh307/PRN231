using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SPHealthSupportSystem_Repositories.Models;
using SPHealthSupportSystem_Services;

namespace SPHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly UserAccountService _userAccountService;
        private readonly IConfiguration _configuration;
        public UserAccountsController(UserAccountService userAccountService, IConfiguration configuration)
        {
            _userAccountService = userAccountService;
            _configuration = configuration;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userAccountService.Authenticate(request.UserName, request.Password);

            if (user == null || user.Result == null)
                return Unauthorized();
            
            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(UserAccount userAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"]                    
                    , _configuration["Jwt:Audience"]
                    , new Claim[]
                    {
                        new(ClaimTypes.Name, userAccount.UserName),
                        //new(ClaimTypes.Email, systemUserAccount.Email),
                        new(ClaimTypes.Role, userAccount.RoleId.ToString()),                        
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials                
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string UserName, string Password);
    }
}