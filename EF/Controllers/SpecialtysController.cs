using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    public class SpecialtysController : Controller
    {
        public static List<Specialty> Specialtys = new List<Specialty>{
            new Specialty(1, "Вычислительная техника и программное обеспечение", "5B070400", UsersController.Users[0], DateTime.Now.ToString()),
            new Specialty(2, "Информатика", "5B070200", UsersController.Users[0], DateTime.Now.ToString())
        };
        // GET: api/values
        [HttpGet]
        public IEnumerable<Object> Get()
        {
            return Specialtys;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            return Specialtys[id - 1];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Specialty value)
        {
            value.Id = Specialtys.Count() + 1;
            value.Date = DateTime.Now.ToString();
            Specialtys.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Specialty value)
        {
            value.UpdateDate = DateTime.Now.ToString();
            Specialtys[id - 1] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Specialtys.Remove(Specialtys[id - 1]);
        }
    }
}
