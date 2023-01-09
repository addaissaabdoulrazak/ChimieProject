using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ChimieProject.Models.Entities
{
    public class User
    {
        public int Id { get; set; }


        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } =string.Empty;

        public string ConfirmPassword { get; set; }


        public string Email { get; set; } = string.Empty;

        public string Role { get; set; }  

     
    }
}
