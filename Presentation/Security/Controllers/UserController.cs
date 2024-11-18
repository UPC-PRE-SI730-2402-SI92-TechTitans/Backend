using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Security.Resources;

namespace Presentation.Security.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Gets the user value by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A string as a sample value.</returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}