namespace API.DTO
{
    public class AuthRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthRequestModel()
        {
        }

        public AuthRequestModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
