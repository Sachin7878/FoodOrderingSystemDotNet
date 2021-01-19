using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [Route("account")]
        //GET : /api/UserProfile
        public async Task<UserModel> FetchUserAccount() {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new UserModel(user);
        }
    }
}
