using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WatchTracker.Api.Services;

namespace WatchTracker.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration config, IEmailService emailService)
        {
            _userManager = userManager;
            _config = config;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(
                nameof(ConfirmEmail),
                "Auth",
                new { userId = user.Id, token = token },
                Request.Scheme);

            return await SendConfirmationEmail(model.Email, confirmationLink);
        }

        /// <summary>
        /// Resend email without adding user to database 
        /// </summary>
        [HttpGet("resend")]
        public async Task<IActionResult> ResendEmail(string email)
        {
            // Email confirmation token
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(
                nameof(ConfirmEmail),
                "Auth",
                new { userId = user.Id, token = token },
                Request.Scheme);

            return await SendConfirmationEmail(email, confirmationLink);
        }

        private async Task<IActionResult> SendConfirmationEmail(string email, string? confirmationLink)
        {
            await _emailService.SendEmailAsync(
                    email,
                    "Potwierdź konto",
                    $"Kliknij link: <a href='{confirmationLink}'>Potwierdź email</a>"
                );

            return Ok();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Invalid user");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
                return BadRequest("Invalid token");

            return Ok("Email confirmed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return Unauthorized("Email not confirmed");
                }

                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtKey = _config["Jwt:Key"];
            var jwtIssuer = _config["Jwt:Issuer"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public record RegisterModel(string Email, string Password);
    public record LoginModel(string Email, string Password);
}
