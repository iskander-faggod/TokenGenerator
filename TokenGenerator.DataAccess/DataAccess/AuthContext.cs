using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TokenGenerator.Domain.Models;
using TokenGenerator.Domain.Models.AuthEntities;
using DbContext = System.Data.Entity.DbContext;

namespace TokenGenerator.DataAccess.DataAccess;

public class AuthContext : IdentityDbContext<TokenIdentityUser, TokenIdentityRole, Guid>
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}