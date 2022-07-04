using Microsoft.AspNetCore.Identity;

namespace TokenGenerator.Domain.Models.AuthEntities;

public class TokenIdentityRole : IdentityRole<Guid>
{
    public const string Admin = "Admin";
    public const string User = "User";

    public TokenIdentityRole(string roleName)
        : base(roleName)
    {
    }

    protected TokenIdentityRole() { }
}