using Microsoft.AspNetCore.Identity;


namespace BookStore.Core.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayName { get; set; } 
    }
}
