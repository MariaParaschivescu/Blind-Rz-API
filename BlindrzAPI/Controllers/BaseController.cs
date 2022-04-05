using Microsoft.AspNetCore.Mvc;

namespace BlindrzAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
