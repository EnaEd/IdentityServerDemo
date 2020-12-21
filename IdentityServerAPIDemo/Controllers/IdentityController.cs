using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IdentityServerAPIDemo.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet("api/user")]
        public IActionResult GetUserClaim()
        {
            return new JsonResult(User.Claims.ToList().Select(x => new { x.Type, x.Value }));
        }

        [HttpGet("api/items")]
        [AllowAnonymous]
        public IActionResult GetItems()
        {
            return Ok(new TestModel());

        }
    }
}
