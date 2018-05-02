using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Bearer")]
    public class SubjectsController : Controller
    {
        //public SubjectsController()
        //{
        //    var _context = new RypDbContext();

        //     //Create subjects
        //    using(var context = new RypDbContext())
        //    {

        //        context.SubjectTypes.Add(new SubjectType { Name = "Базовая", Color = "#64B5F6", Code = "Б" });
        //        context.SubjectTypes.Add(new SubjectType { Name = "Профилирующая", Color = "#7986CB", Code = "П" });
        //        context.SubjectTypes.Add(new SubjectType { Name = "Общеобразовательная", Color = "#81C784", Code = "О" });
        //        context.SaveChanges();

        //        //var type = context.SubjectTypes.FirstOrDefault(x => x.Name == "Общеобразовательная");
        //        //var newSubj = new Subject("Физика 2", type.Id, 1, 2, 0, "f1", "9129d902-1aa4-45ce-af78-0fe8984bcddd", DateTime.Now.ToString());
        //        //context.Subjects.Add(newSubj);
        //        //context.SaveChanges();

        //        //var obj = context.Subjects.FirstOrDefault(x => x.Name == "Физика 1");   
        //        //if (obj != null)
        //        //{
        //        //    obj.Parent = newSubj;
        //        //}

        //        //context.SaveChanges();

        //        ////context.Specialties.Add(new Specialty
        //        ////{
        //        ////    Name = "Вычислительная техника и программное обеспечение",
        //        ////    Shifr = "5B070400",
        //        ////    UserId = "9129d902-1aa4-45ce-af78-0fe8984bcddd",
        //        ////    Date = DateTime.Now.ToString()
        //        ////});
        //        ////context.SaveChanges();
        //    }
        //}

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            List<SubjectVM> result = null;
            using (var ctx = new RypDbContext())
            {
                result = ctx.Subjects.Select(x => new SubjectVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Shifr = x.Shifr,
                    Credits = x.Credits,
                    Lec = x.Lec,
                    Lab = x.Lab,
                    Pr = x.Pr,
                    UserId = x.UserId,
                    Date = x.Date,
                    UpdateDate = x.UpdateDate,
                    Type = x.Type,
                    Deleted = x.Deleted,
                    Prerequisites = x.RelatedItems.Select(y => new SubjectVM
                    {
                        Id = y.Related.Id,
                        Name = y.Related.Name,
                        Shifr = y.Related.Shifr,
                        Credits = y.Related.Credits,
                        Lec = y.Related.Lec,
                        Lab = y.Related.Lab,
                        Pr = y.Related.Pr,
                        UserId = y.Related.UserId,
                        Date = y.Related.Date,
                        UpdateDate = y.Related.UpdateDate,
                        Type = y.Related.Type
                    }).ToList()
                }).Where(x => x.Deleted == 0).ToList();
            }
           
            return Ok(result);
        }

         //POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SubjectVM model)
        {
            using(var context = new RypDbContext()) 
            {
                var type = context.SubjectTypes.FirstOrDefault(x => x.Name == model.Type.Name);

                var subject = new Subject();
                subject.RelatedItems = new List<SubjectPrerequisiteSubject>();
                subject.Name = model.Name;
                subject.Shifr = model.Shifr;
                subject.Credits = model.Credits;
                subject.Lec = model.Lec;
                subject.Lab = model.Lab;
                subject.Pr = model.Pr;
                subject.UserId = model.UserId;
                subject.Date = DateTime.Now.ToString();
                subject.TypeId = type.Id;

                for (int i = 0; i < model.Prerequisites.Count; i++)
                {
                    subject.RelatedItems.Add(new SubjectPrerequisiteSubject { Primary = subject, RelatedId = model.Prerequisites[i].Id });
                }
                //subject.Prerequisites = model.Prerequisites;
                //model.Prerequisites.ForEach(p =>{
                //    context.Attach(p);
                //});

                var result = await context.Subjects.AddAsync(subject);
                context.SaveChanges();
            }
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SubjectVM model)
        {
            using (var context = new RypDbContext())
            {
                var type = context.SubjectTypes.FirstOrDefault(x => x.Name == model.Type.Name);

                var subject = context.Subjects.FirstOrDefault(x => x.Id == id);
                var prerequisites = context.SubjectPrerequisiteSubjects.Where(x => x.PrimaryId == subject.Id).ToList();
                subject.RelatedItems = prerequisites;
                subject.Name = model.Name;
                subject.Shifr = model.Shifr;
                subject.Credits = model.Credits;
                subject.Lec = model.Lec;
                subject.Lab = model.Lab;
                subject.Pr = model.Pr;
                subject.UpdateDate = DateTime.Now.ToString();
                subject.TypeId = type.Id;

                for (int i = subject.RelatedItems.Count; i > 0; i--)
                {
                    //subject.RelatedItems.Remove(subject.RelatedItems[i - 1]);
                    context.SubjectPrerequisiteSubjects.Remove(subject.RelatedItems[i - 1]);
                }
                context.SaveChanges();
                subject.RelatedItems = new List<SubjectPrerequisiteSubject>();

                for (int i = 0; i < model.Prerequisites.Count; i++)
                {
                    subject.RelatedItems.Add(new SubjectPrerequisiteSubject { Primary = subject, RelatedId = model.Prerequisites[i].Id });
                }

                context.SaveChanges();
            }

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var context = new RypDbContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    var subject = await context.Subjects.FirstOrDefaultAsync(x => x.Id == id);
                    subject.Deleted = 1;
                    await context.SaveChangesAsync();
                    tran.Commit();
                }
            }
            return Ok();
        }
    }
}
