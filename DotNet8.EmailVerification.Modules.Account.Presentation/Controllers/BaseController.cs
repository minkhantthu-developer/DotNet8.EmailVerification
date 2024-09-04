using DotNet8.EmailVerification.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNet8.EmailVerification.Modules.Account.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Content(Object obj)
        {
            return Content(obj.ToJson(), "application/json");
        }
    }
}
