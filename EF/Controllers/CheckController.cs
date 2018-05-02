using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Policy = "Bearer")]
    public class CheckController : Controller
    {
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<RypVM> result = new List<RypVM>();
            using (var ctx = new RypDbContext())
            {
                result = await ctx.Ryps.Select(x => new RypVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    Date = x.Date,
                    UpdateDate = x.UpdateDate,
                    Specialty = x.Specialty,
                    Year = x.Year,
                    OperatorCheck = x.OperatorCheck,
                    FullCheck = x.FullCheck,
                    Prototype = x.Prototype,
                    Deleted = x.Deleted,
                    Semesters = x.Semesters.Select(y => new SemesterVM
                    {
                        Subjects = y.SemesterSubjects.Select(z => new SubjectVM
                        {
                            Id = z.Subject.Id,
                            Name = z.Subject.Name,
                            Shifr = z.Subject.Shifr,
                            Credits = z.Subject.Credits,
                            Lec = z.Subject.Lec,
                            Lab = z.Subject.Lab,
                            Pr = z.Subject.Pr,
                            UserId = z.Subject.UserId,
                            Date = z.Subject.Date,
                            UpdateDate = z.Subject.UpdateDate,
                            Type = z.Subject.Type,
                            Prerequisites = z.Subject.RelatedItems.Select(a => new SubjectVM
                            {
                                Id = a.Related.Id,
                                Name = a.Related.Name,
                                Shifr = a.Related.Shifr,
                                Credits = a.Related.Credits,
                                Lec = a.Related.Lec,
                                Lab = a.Related.Lab,
                                Pr = a.Related.Pr,
                                UserId = a.Related.UserId,
                                Date = a.Related.Date,
                                UpdateDate = a.Related.UpdateDate,
                                Type = a.Related.Type
                            }).ToList()
                        }).ToList(),
                        Electives = y.SemesterElectiveGroups.Select(z => new ElectiveGroupVM
                        {
                            Id = z.ElectiveGroup.Id,
                            Name = z.ElectiveGroup.Name,
                            Shifr = z.ElectiveGroup.Shifr,
                            Credits = z.ElectiveGroup.Credits,
                            Pr = z.ElectiveGroup.Pr,
                            UserId = z.ElectiveGroup.UserId,
                            Date = z.ElectiveGroup.Date,
                            UpdateDate = z.ElectiveGroup.UpdateDate,
                            Type = z.ElectiveGroup.Type,
                            Subjects = z.ElectiveGroup.SubjectElectiveGroups.Select(a => new SubjectVM
                            {
                                Id = a.Subject.Id,
                                Name = a.Subject.Name,
                                Shifr = a.Subject.Shifr,
                                Credits = a.Subject.Credits,
                                Lec = a.Subject.Lec,
                                Lab = a.Subject.Lab,
                                Pr = a.Subject.Pr,
                                UserId = a.Subject.UserId,
                                Date = a.Subject.Date,
                                UpdateDate = a.Subject.UpdateDate,
                                Type = a.Subject.Type,
                                Prerequisites = a.Subject.RelatedItems.Select(b => new SubjectVM
                                {
                                    Id = b.RelatedId,
                                    Name = b.Related.Name,
                                    Shifr = b.Related.Shifr,
                                    Credits = b.Related.Credits,
                                    Lec = b.Related.Lec,
                                    Lab = b.Related.Lab,
                                    Pr = b.Related.Pr,
                                    UserId = b.Related.UserId,
                                    Date = b.Related.Date,
                                    UpdateDate = b.Related.UpdateDate,
                                    Type = b.Related.Type
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).Where(x => x.FullCheck == 0 && x.Deleted == 0).ToListAsync();
            }
            return Ok(result);
        }
        // POST api/values
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Post([FromBody]int id)
        {
            using (var context = new RypDbContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    var ryp = context.Ryps.FirstOrDefault(x => x.Id == id);
                    ryp.OperatorCheck = 1;

                    context.SaveChanges();
                    tran.Commit();
                }
            }
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]RypVM model)
        {
            using (var context = new RypDbContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    var ryp = context.Ryps.FirstOrDefault(x => x.Id == id);
                    ryp.OperatorCheck = 0;

                    context.SaveChanges();
                    tran.Commit();
                }
            }
            return Ok();
        }
    }
}
