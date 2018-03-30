using System;
namespace EF.Models
{
    public enum UserRole
    {
        mg = 0,
        or = 1,
        op = 2
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
