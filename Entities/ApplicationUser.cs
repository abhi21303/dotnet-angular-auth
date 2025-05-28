using Microsoft.AspNetCore.Identity;

namespace DotNetWebApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Created_DateTime {  get; set; } 
        public DateTime? Updated_DateTime { get; set; }  
    }
}
