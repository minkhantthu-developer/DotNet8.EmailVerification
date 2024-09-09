namespace DotNet8.EmailVerification.DTOs.Features.Account;

public class RegisterUserDTO
{
    public string? UserName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public string? Password { get;set; }
}
