using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthenticationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(AuthenticationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("account")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAccount()
        {
            var applicationUser = await _userManager.GetUserAsync(User);
            var currentUser = new UserModel(applicationUser);
            return Ok(currentUser);
        }
    }
}
