using Microsoft.AspNetCore.Identity;

namespace Chat.Web.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
