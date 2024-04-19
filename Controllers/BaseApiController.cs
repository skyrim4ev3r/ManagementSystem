using Microsoft.AspNetCore.Mvc;
using Project.Core;

namespace Project.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.ResultCode == 200)
                return Ok(result);
            if (result.ResultCode == 400 || result.ResultCode == -400)
                return BadRequest(result);
            if (result.ResultCode == 404 || result.ResultCode == -404)
                return NotFound(result);
            if (result.ResultCode == -1400)
                return BadRequest(result);
            return BadRequest(result);
        }
    }
}
