using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Tsc.Application.ServiceModel;

namespace Client.Controllers
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]UserPasswordCredentials credentials)
        {
            if (credentials == null)
            {
                return HttpBadRequest();
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
                result = HttpUnauthorized();
            }

            return result;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody]UserPasswordCredentials credentials)
        {
            if (credentials == null)
            {
                return HttpBadRequest();
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
                result = HttpUnauthorized();
            }

            return result;
        }
    }
}
