using Domain.Security.Model.Commands;
using Domain.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Security.Resources;
using Presentation.Security.Transform;

namespace Presentation.Security.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="signUpResource">The user's registration data.</param>
        /// <returns>An action result indicating if the registration was successful.</returns>
        [HttpPost("register")]       
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] SignUpResource signUpResource)
        {
            var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);

            await userCommandService.Handle(command);

            return StatusCode(201, command);
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="signInResource">The user's login data.</param>
        /// <returns>A message indicating if the login was successful.</returns>
        [HttpPost("login")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] SignInResource signInResource)
        {
            var commad = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
            
            var response =   await userCommandService.Handle(commad);
            
            return Ok(new { message = "User created successfully",token = response.token , userId = response.user.Id });
        }
    }
}