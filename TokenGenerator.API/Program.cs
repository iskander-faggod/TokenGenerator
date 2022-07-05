using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TokenGenerator.DataAccess.DataAccess;
using TokenGenerator.Domain.Models.AuthEntities;
using TokenGenerator.Services.Interfaces;
using TokenGenerator.Services.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration["ConnectionStrings:connectionString"];
var defaultCorsPolicyName = "DefaultName";


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IValuteService, ValuteService>();

builder.Services.AddDbContext<AuthContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(connectionString);
});

builder.Services.AddIdentity<TokenIdentityUser, TokenIdentityRole>()
    .AddEntityFrameworkStores<AuthContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        defaultCorsPolicyName,
        builder =>
        {
            builder.WithOrigins(
                    configuration.GetSection("http://localhost:3000")
                        .GetChildren()
                        .Select(x => x.Value)
                        .ToArray())
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(defaultCorsPolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();