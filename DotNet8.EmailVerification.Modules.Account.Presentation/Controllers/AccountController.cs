using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Modules.Account.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.EmailVerification.Modules.Account.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) => _userService = userService;

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromForm]
            RegisterUserDTO requestModel,
            CancellationToken cancellationToken
            )
        {
            var response=await _userService.RegisterUserAsync(requestModel, cancellationToken);
            return Content(response);
        }
    }
}
