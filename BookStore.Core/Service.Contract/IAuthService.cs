using BookStore.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Core.Service.Contract
{
    public interface IAuthService
    {
        Task<string> CreateTokeAync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
