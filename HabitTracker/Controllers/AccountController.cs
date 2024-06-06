using HabitTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HabitTracker.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [Inject]
        HabitAppContext HabitAppContext { get; set; }


        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            var redirectUrl = Url.Action(nameof(GoogleResponse));
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Principal == null)
            {
                return BadRequest("Google authentication failed.");
            }

            var claims = result.Principal.Identities
                .FirstOrDefault()?.Claims
                .Select(claim => new
                {
                    claim.Type,
                    claim.Value
                })
                .ToList();

            var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (emailClaim == null)
            {
                return BadRequest("Google authentication did not return an email.");
            }

            var userEmail = emailClaim.Value;
            var userName = nameClaim?.Value ?? "Unknown";

            // Check if user exists in the database
            var user = await HabitAppContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);


            if (user == null)
            {
                user = new User
                {
                    Email = userEmail,
                    UserName = userName,
                };

                await HabitAppContext.Users.AddAsync(user);
                await HabitAppContext.SaveChangesAsync();
            }

            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok("User is logged in");
        }

    }
}
