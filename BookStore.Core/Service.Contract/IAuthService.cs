using BookStore.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Service.Contract
{
    public interface IAuthService
    {
        Task<string> CreateTokeAync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
