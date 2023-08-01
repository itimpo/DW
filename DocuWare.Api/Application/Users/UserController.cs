using DocuWare.Abstractions;
using DocuWare.Abstractions.User;
using DocuWare.Web.BackendApp.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DocuWare.Web.Controllers.Users;

[ApiController]
[Route("[controller]"), AllowAnonymous]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserController(
        IUserRepository userRepository, 
        IConfiguration configuration,
        ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// User Login using email and password
    /// </summary>
    /// <response code="200">Successful operation</response>
    /// <response code="400">Invalid post model</response>
    /// <response code="401">User not found</response>
    [ProducesResponseType(typeof(UserToken), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLogin model)
    {
        if (model is null)
        {
            return BadRequest(new Result { Error = "Invalid user request!!!" });
        }
        var user = await _userRepository.GetUser(model.Email, model.Password);
        if (user!=null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"], 
                audience: _configuration["JWT:ValidAudience"], 
                claims: claims, 
                expires: DateTime.Now.AddDays(6), 
                signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new UserToken
            {
                Token = tokenString
            });
        }
        return Unauthorized();
    }
}