using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
            : base() { }

        public AppUser(string userName)
            : base(userName) { }

        public int? Age { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePic { get; set; }
        public FullName FullName { get; set; }
    }
}
