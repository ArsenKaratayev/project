using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    public class RypsController : Controller
    {
        public static List<Ryp> Ryps = new List<Ryp>();

        // GET: api/values
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return Ryps;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return Ryps[id - 1];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Ryp value)
        {
            value.Id = Ryps.Count() + 1;
            value.Date = DateTime.Now.ToString();
            Ryps.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Ryp value)
        {
            value.UpdateDate = DateTime.Now.ToString();
            Ryps[id - 1] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Ryps.Remove(Ryps[id - 1]);
        }
    }
}
