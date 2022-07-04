using System.ComponentModel.DataAnnotations;

namespace TokenGenerator.Domain.Models.AuthEntities;

public class LoginModel
{
    public Guid Id { get; protected init; } = Guid.NewGuid();

    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    public override int GetHashCode() => HashCode.Combine(Id, Email);
    public override bool Equals(object? obj) => Equals(obj as LoginModel);

    private bool Equals(LoginModel? login) =>
        login is not null &&
        Id.Equals(login.Id);
}