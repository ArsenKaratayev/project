using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public static List<Subject> Subjects = new List<Subject>{
            new Subject(1, "Физика 1", "Основная", new SubjectHours(1, 2, 0), 3, "f1"),
            new Subject(2, "Математика", "Основная", new SubjectHours(2, 1, 1), 4, "m1"),
            new Subject(3, "Информатика", "Основная", new SubjectHours(2, 1, 0), 3, "i1"),
            new Subject(4, "История", "Основная", new SubjectHours(2, 0, 1), 3, "h1"),
            new Subject(5, "АиОП", "Основная", new SubjectHours(2, 2, 0), 4, "p1"),
            new Subject(6, "АиСД", "Основная", new SubjectHours(2, 1, 0), 3, "a1"),
        };
        public static List<Specialty> Specialtys = new List<Specialty>{
            new Specialty(1, "ВТиПО", "5B070400", Subjects),
            new Specialty(2, "ВТиПО", "5B070400", Subjects),
            new Specialty(3, "ВТиПО", "5B070400", Subjects)
        };
        public static List<Ryp> Ryps = new List<Ryp>();

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/id
        [HttpGet("{id}")]
        public IEnumerable<Object> Get(int id)
        {
            if (id == 1)
            {
                return Subjects;
            } else if (id == 2)
            {
                return Specialtys;
            } else if (id == 3)
            {
                return Ryps;
            } else
            {
                return new List<string>{ "Сорри, ошибка"};
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Subject value)
        {
            value.Id = Subjects.Count() + 1;
            Subjects.Add(value);
        }
        //[HttpPost]
        //[Route("api/values/addsubject")]
        //public void addsubject([FromBody]Subject value)
        //{
        //    value.Id = Subjects.Count() + 1;
        //    Subjects.Add(value);
        //}
        //[HttpPost]
        //public void Post([FromBody]Specialty value)
        //{
        //    Specialtys.Add(value);
        //}
        //[HttpPost]
        //public void Post([FromBody]Ryp value)
        //{
        //    Ryps.Add(value);
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
