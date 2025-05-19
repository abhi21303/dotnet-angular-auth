using Microsoft.AspNetCore.Identity;

namespace DotNetWebApi.Entitie
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Created_DateTime {  get; set; } 
        public DateTime? Updated_DateTime { get; set; }  
    }
}
