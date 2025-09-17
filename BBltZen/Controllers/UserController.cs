using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BBltZen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly JwtServizio _jwtServizio;

        public UserController(JwtServizio jwtServizio)
        {
            _jwtServizio = jwtServizio;
        }
        [HttpGet("info")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var (username, utenteId) = _jwtServizio.GetInfoToken();
            if (username == null)
                return Unauthorized();
            return Ok(new { Username = username, UtenteId = utenteId });
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // In un'applicazione reale, dovresti verificare le credenziali dell'utente
            if (request.Username == "test" && request.Password == "password")
            {
                var token = _jwtServizio.CreateToken(request.Username, 1); // Supponiamo che l'ID utente sia 1
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
