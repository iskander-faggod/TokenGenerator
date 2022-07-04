using Microsoft.AspNetCore.Identity;

namespace TokenGenerator.Domain.Models.AuthEntities;

public class TokenIdentityUser : IdentityUser<Guid>
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
