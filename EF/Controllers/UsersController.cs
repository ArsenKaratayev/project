using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public static List<User> Users = new List<User>
        { 
            new User{ Id = 1, Name = "mg", Password = "123", Role = UserRole.mg },
            new User{ Id = 2, Name = "or", Password = "123", Role = UserRole.or },
            new User{ Id = 3, Name = "op", Password = "123", Role = UserRole.op },
        };

        // GET: api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            return Users[id - 1];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]User value)
        {
            value.Id = Users.Count() + 1;
            Users.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User value)
        {
            Users[id - 1] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Users.Remove(Users[id - 1]);
        }
    }
}
