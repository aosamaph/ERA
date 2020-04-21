using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models.Respository
{
   public interface IUserRespository
    {
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<AppUser> FindByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<AppUser> FindByIdAsync(string userId);
        Task<IdentityResult> UpdateAsync(AppUser user);
    }
}
