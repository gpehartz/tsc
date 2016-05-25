using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tsc.WebApi.ServiceModel;

namespace Tsc.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserPasswordCredentials credentials)
        {
            if (credentials == null)
            {
                return BadRequest();
            }

            IActionResult result;
            var signInResult = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);
            if (signInResult.Succeeded)
            {
                Task<User> userTask = _userManager.FindByNameAsync(credentials.Email);
                var user = userTask.Result;
                result = Ok(user);
            }
            else
            {
                result = Unauthorized();
            }

            return result;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserPasswordCredentials credentials)
        {
            if (credentials == null)
            {
                return BadRequest();
            }

            var user = 
                new User
                {
                    UserName = credentials.Email,
                    Email = credentials.Email
                };

            IActionResult result;
            var createResult = await _userManager.CreateAsync(user, credentials.Password);
            if (createResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                result = Ok();
            }
            else
            {
                result = Unauthorized();
            }

            return result;
        }
    }
}
