namespace DotNet8.EmailVerification.Modules.Account.Presentation.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : BaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService) => _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(
        RegisterUserDTO requestModel,
        CancellationToken cancellationToken
        )
    {
        var response=await _userService.RegisterUserAsync(requestModel, cancellationToken);
        return Content(response);
    }

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfrimEmailAsync(
        ConfirmEmailDTO confirmEmailDto,
        CancellationToken cancellationToken
        )
    {
        var response=await _userService.ConfirmEmailAsync(confirmEmailDto, cancellationToken);
        return Content(response);
    }
}
