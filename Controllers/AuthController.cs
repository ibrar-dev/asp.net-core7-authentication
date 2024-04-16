using AuthenticationApp.DB;
using AuthenticationApp.DTOs;
using AuthenticationApp.Models;
using AuthenticationApp.Responses;
using AuthenticationApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationApp.Responses;
using AuthenticationApp.Services.RefreshToken;
using Microsoft.Extensions.Options;

namespace AuthenticationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly AuthenticationAPIContext _context;
        private readonly IUserService _userService;
        private readonly IRefreshTokenGenerator tokenGenerator;
        private readonly JWTSetting setting;
        public AuthController(
            IConfiguration configuration, 
            AuthenticationAPIContext Auth_DB, 
            IUserService userService, 
            IRefreshTokenGenerator _refreshToken,
            IOptions<JWTSetting> options
            )
        {
            _configuration = configuration;
            _context = Auth_DB;
            _userService = userService;
            tokenGenerator = _refreshToken;
            setting = options.Value;

        }




        [HttpPost("register")]
        public async Task<ActionResult<RegisterRequest>> Register(RegisterRequest request)
        {
            var _user = _context.User.FirstOrDefault(o => o.Username == request.Username);
            if (_user!= null)
            {
                return BadRequest("User Already Exist.");
            }

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user = new User();
            user.PhoneNumber = request.PhoneNumber;
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.Email = request.Email;

            var result =  await _userService.AddUser(user);
            if (result is null)
                return NotFound("User not found.");
            return Ok(result);
        }

        [HttpPost("login")]
        public ActionResult<TokenResponse> Login(LoginRequestDto request)
        {

            var _user = _context.User.FirstOrDefault(o => o.Username == request.Username);
            if (_user == null)
            {
                return Unauthorized();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, _user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            TokenResponse token = CreateToken(_user);
           

            return Ok(token);
        }

        [Route("Refresh")]
        [HttpPost]
        public IActionResult Refresh([FromBody] TokenResponse token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token.JWTToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.securitykey)),
                ValidateIssuer = false,
                ValidateAudience = false,

            }, out securityToken);

            var _token = securityToken as JwtSecurityToken;
            if (_token != null && !_token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return Unauthorized();
            }
            var username = principal.Identity.Name;
            var _reftable = _context.User.FirstOrDefault(o => o.Username == username );
            if (_reftable == null)
            {
                return Unauthorized();
            }

            TokenResponse _result = Authenticate(username, principal.Claims.ToArray());

            return Ok(_result);
        }

        [NonAction]
        public TokenResponse Authenticate(string username, Claim[] claims)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var tokenkey = Encoding.UTF8.GetBytes(setting.securitykey);
            var tokenhandler = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)

                );
            tokenResponse.JWTToken = new JwtSecurityTokenHandler().WriteToken(tokenhandler);
            tokenResponse.RefreshToken = tokenGenerator.GenerateToken(username);

            return tokenResponse;
        }

        private TokenResponse CreateToken(User user)
        {
            TokenResponse tokenResponse = new TokenResponse();

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            tokenResponse.JWTToken = jwt;
            tokenResponse.RefreshToken = tokenGenerator.GenerateToken(user.Username);
            return tokenResponse;
        }

    }
}
