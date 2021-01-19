using API.Models;

namespace API.DTO
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public Address Address { get; set; }

        public UserModel(ApplicationUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            MobileNo = user.PhoneNumber;
            Address = user.Address;
        }
    }
}
