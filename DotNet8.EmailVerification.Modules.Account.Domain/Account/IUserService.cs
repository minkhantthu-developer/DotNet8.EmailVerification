namespace DotNet8.EmailVerification.Modules.Account.Domain.Account;

public interface IUserService
{
    Task<Result<UserDTO>> RegisterUserAsync(RegisterUserDTO requestModel,
        CancellationToken cancellationToken);

    Task<Result<UserDTO>> ConfirmEmailAsync(ConfirmEmailDTO comfirmEmail,
        CancellationToken cancellationToken);
}
