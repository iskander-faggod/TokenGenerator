using System.IdentityModel.Tokens.Jwt;
using TokenGenerator.Domain;
using TokenGenerator.Domain.Models.AuthEntities;

namespace TokenGenerator.Services.Interfaces;

public interface IAuthService
{
    Task<JwtSecurityToken> Login(LoginModel model);
    Task Register(RegisterModel model);
}