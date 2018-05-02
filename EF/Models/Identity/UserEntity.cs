using System;
using Microsoft.AspNetCore.Identity;

namespace EF.Models
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserEntity()
        {
        }
    }
}
