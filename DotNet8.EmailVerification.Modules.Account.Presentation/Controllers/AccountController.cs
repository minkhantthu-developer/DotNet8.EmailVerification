using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8.EmailVerification.Modules.Account.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        [HttpGet]
        public IActionResult Test(CancellationToken cancellationToken)
        {
            return Content("cancellationToken");
        }
    }
}
