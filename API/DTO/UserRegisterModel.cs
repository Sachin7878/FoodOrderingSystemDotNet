namespace API.DTO
{
    public class UserRegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string _role;

        public string Role
        {
            get => _role;
            set => _role = string.IsNullOrEmpty(value) ? "Customer" : value;
        }

        public UserRegisterModel()
        {
            
        }
        public UserRegisterModel(string userName, string email, string password, string firstName, string lastName)
        {
            UserName = userName;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;    
        }
    }
}
