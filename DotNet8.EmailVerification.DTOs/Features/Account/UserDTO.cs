﻿namespace DotNet8.EmailVerification.DTOs.Features.Account;

public class UserDTO
{
    public string UserId { get; set; }

    public string? UserName { get; set; }
                 
    public string? Email { get; set; }
                 
    public string? Password { get; set; }

    public bool IsActive { get; set; }

    public bool IsEmailConfirmed { get; set; }
}
