using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    public class SubjectsController : Controller
    {
        public static List<Subject> Subjects = new List<Subject> {
            new Subject(1, "Физика 1", new SubjectType("Общеобразовательная"), new SubjectHours(1, 2, 0), "f1", UsersController.Users[0], DateTime.Now.ToString()),
            new Subject(2, "Математика", new SubjectType("Общеобразовательная"), new SubjectHours(2, 1, 1), "m1", UsersController.Users[1], DateTime.Now.ToString()),
            new Subject(3, "Информатика", new SubjectType("Общеобразовательная"), new SubjectHours(2, 1, 0), "i1", UsersController.Users[2], DateTime.Now.ToString()),
            new Subject(4, "История", new SubjectType("Общеобразовательная"), new SubjectHours(2, 0, 1), "h1", UsersController.Users[0], DateTime.Now.ToString()),
            new Subject(5, "АиОП", new SubjectType("Базовая"), new SubjectHours(2, 2, 0), "p1", UsersController.Users[0], DateTime.Now.ToString()),
            new Subject(6, "АиСД", new SubjectType("Профилирующая"), new SubjectHours(2, 1, 0), "a1", UsersController.Users[1], DateTime.Now.ToString()),
            new Subject(7, "ИГС", new SubjectType("Профилирующая"), new SubjectHours(2, 1, 0), "g1", UsersController.Users[1], DateTime.Now.ToString()),
            new Subject(8, "СТП", new SubjectType("Профилирующая"), new SubjectHours(2, 1, 0), "s1", UsersController.Users[2], DateTime.Now.ToString()),
        };

        // GET: api/values
        [HttpGet]
        public IEnumerable<Object> Get()
        {
            return Subjects;
        }

        // GET api/values/id
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            return Subjects[id - 1];
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Subject value)
        {
            value.Id = Subjects.Count() + 1;
            value.Date = DateTime.Now.ToString();
            Subjects.Add(value);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Subject value)
        {
            value.UpdateDate = DateTime.Now.ToString();
            Subjects[id - 1] = value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Subjects.Remove(Subjects[id - 1]);
        }
    }
}
