using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class AuthResponseModel
    {
        public string Token { get; set; }
        public string ExpiresIn { get; set; }
        public string Role { get; set; }

        public AuthResponseModel(string token, string expiresIn, string role)
        {
            Token = token;
            ExpiresIn = expiresIn;
            Role = role;
        }

        public AuthResponseModel()
        {
        }
    }
}
