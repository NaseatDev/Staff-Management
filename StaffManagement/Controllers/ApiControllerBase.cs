using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace StaffManagement.Api.Controllers
{
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
    }
}
