using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models.Respository
{
    public class UserRespository : IUserRespository
    {
        public Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
