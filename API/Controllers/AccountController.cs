using Microsoft.AspNetCore.Mvc;
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
        public async Task<UserModel> FetchUserAccount() {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new UserModel(user);
        }

        [HttpPost]
        [Authorize]
        [Route("account/address")]
        public async Task<UserModel> AddUserAddress(Address newAddress) {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            user.Address = newAddress;

            await _context.SaveChangesAsync();
            return new UserModel(user);
        }

        [HttpPut]
        [Authorize]
        [Route("account")]
        public async Task<OkResult> UpdateAccount(UserModel userAccount)
        {
            var userFromDb = await _context.ApplicationUsers.FindAsync(userAccount.Id);
            userFromDb.FirstName = userAccount.FirstName;
            userFromDb.LastName = userAccount.LastName;
            userFromDb.PhoneNumber = userAccount.MobileNo;
            userFromDb.Email = userAccount.Email;
            userFromDb.NormalizedEmail = userAccount.Email.ToUpper();
            userFromDb.UserName = userAccount.Email;
            userFromDb.NormalizedUserName = userAccount.Email.ToUpper();

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("account/address")]
        public async Task<OkResult> UpdateUserAddress(Address userAddress)
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            user.Address.AddressLine1 = userAddress.AddressLine1;
            user.Address.AddressLine2 = userAddress.AddressLine2;
            user.Address.PinCode = userAddress.PinCode;
            user.Address.City = userAddress.City;
            user.Address.State = userAddress.State;
            user.Address.Country = userAddress.Country;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
