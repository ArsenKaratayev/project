using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    public class ElectiveGroupsController : Controller
    {
        public static List<ElectiveGroup> Electives = new List<ElectiveGroup>();

        // GET: api/values
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return Electives;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return Electives[id - 1];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ElectiveGroup value)
        {
            value.Id = Electives.Count() + 1;
            value.Date = DateTime.Now.ToString();
            Electives.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ElectiveGroup value)
        {
            value.UpdateDate = DateTime.Now.ToString();
            Electives[id - 1] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Electives.Remove(Electives[id - 1]);
        }
    }
}
