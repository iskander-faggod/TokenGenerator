using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TokenGenerator.Domain.Models.AuthEntities;
using TokenGenerator.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TokenGenerator.Services.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<TokenIdentityUser> _userManager;
    private readonly RoleManager<TokenIdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<TokenIdentityUser> userManager,
        RoleManager<TokenIdentityRole> roleManager,
        IConfiguration configuration
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<JwtSecurityToken> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        var token = new JwtSecurityToken();
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            token =  GetToken(authClaims);
        }

        return token;
    }

    public async Task Register(RegisterModel model)
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
            throw new NullReferenceException($"{nameof(userExists)} is invalid. Users must be unique");

        TokenIdentityUser user = new()
        {
            Email = model.Email,
            UserName = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ApplicationException($"{nameof(result.Succeeded)} is invalid. Something went wrong with creation new user");
    }
    
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}