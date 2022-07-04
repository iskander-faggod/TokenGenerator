using System.ComponentModel.DataAnnotations;

namespace TokenGenerator.Domain.Models.AuthEntities;

public class RegisterModel
{
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "ConfirmedPassword is required")]
    public string? ConfirmedPassword { get; set; }
}