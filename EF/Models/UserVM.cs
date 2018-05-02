using System;
using System.ComponentModel.DataAnnotations;

namespace EF.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
