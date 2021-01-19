namespace API.DTO
{
    public class VendorRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Role { get; set; }
        public VendorRegisterModel()
        {
            Role = "Vendor";
        }
        public VendorRegisterModel(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = "Vendor";
        }
    }
}
