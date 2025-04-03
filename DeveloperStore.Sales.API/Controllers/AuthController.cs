﻿using DeveloperStore.Sales.API.Models;
using DeveloperStore.Sales.API.Models.Auth;
using DeveloperStore.Sales.API.Settings;
using DeveloperStore.Sales.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeveloperStore.Sales.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    [ApiExplorerSettings(GroupName = "Authentication")]
    [Tags("Authentication")]
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(UserManager<User> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token
        /// </summary>
        /// <param name="request">User credentials</param>
        /// <returns>JWT token for authentication</returns>
        /// <response code="200">Returns the JWT token</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("signin")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 401)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                    return Failure("Invalid email or password", 401);

                var token = GenerateJwtToken(user);
                return Success(new { token }, "Successfully authenticated");
            }
            catch (Exception ex)
            {
                return Failure("An error occurred during authentication", 500);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new("name", user.Name),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}