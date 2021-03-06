﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Bearer")]
    public class RypsController : Controller
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
                    Deleted = x.Deleted,
                    Prototype = x.Prototype,
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
                }).Where(x => x.Deleted == 0).ToListAsync();
            }
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]RypVM model)
        {
            using (var ctx = new RypDbContext())
            {
                using (var tran = ctx.Database.BeginTransaction())
                {
                    var ryp = new Ryp();
                    ryp.Name = model.Name;
                    ryp.Year = model.Year;
                    ryp.UserId = model.UserId;
                    ryp.SpecialtyId = model.Specialty.Id;
                    ryp.Date = DateTime.Now.ToString();
                    ryp.Prototype = model.Prototype;

                    ctx.Ryps.Add(ryp);
                    ctx.SaveChanges();

                    for (int i = 0; i < 8; i++)
                    {
                        var semester = new Semester();
                        semester.RypId = ryp.Id;
                        ctx.Semesters.Add(semester);
                        ctx.SaveChanges();
                        foreach (var subject in model.Semesters[i].Subjects)
                        {
                            var t = new SemesterSubject { SubjectId = subject.Id, SemesterId = semester.Id };
                            ctx.SemesterSubjects.Add(t);
                            semester.SemesterSubjects.Add(t);
                        }
                        foreach (var electiveGroup in model.Semesters[i].Electives)
                        {
                            var t = new SemesterElectiveGroup { ElectiveGroupId = electiveGroup.Id, SemesterId = semester.Id };
                            ctx.SemesterElectiveGroups.Add(t);
                            semester.SemesterElectiveGroups.Add(t);
                        }
                        ryp.Semesters.Add(semester);
                    }
                    ctx.SaveChanges();
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
                    ryp.Semesters = context.Semesters.Where(x => x.RypId == ryp.Id).ToList();
                    ryp.UpdateDate = DateTime.Now.ToString();

                    for (int i = ryp.Semesters.Count; i > 0; i--)
                    {
                        ryp.Semesters[i - 1].SemesterSubjects = context.SemesterSubjects.Where(x => x.SemesterId == ryp.Semesters[i - 1].Id).ToList();
                        ryp.Semesters[i - 1].SemesterElectiveGroups = context.SemesterElectiveGroups.Where(x => x.SemesterId == ryp.Semesters[i - 1].Id).ToList();
                        for (int j = ryp.Semesters[i - 1].SemesterSubjects.Count; j > 0; j--)
                        {
                            context.SemesterSubjects.Remove(ryp.Semesters[i - 1].SemesterSubjects[j - 1]);
                        }
                        for (int j = ryp.Semesters[i - 1].SemesterElectiveGroups.Count; j > 0; j--)
                        {
                            context.SemesterElectiveGroups.Remove(ryp.Semesters[i - 1].SemesterElectiveGroups[j - 1]);
                        }
                        context.Semesters.Remove(ryp.Semesters[i - 1]);
                    }
                    context.SaveChanges();
                    ryp.Semesters = new List<Semester>();

                    for (int i = 0; i < 8; i++)
                    {
                        var semester = new Semester();
                        semester.RypId = ryp.Id;
                        context.Semesters.Add(semester);
                        context.SaveChanges();
                        foreach (var subject in model.Semesters[i].Subjects)
                        {
                            var t = new SemesterSubject { SubjectId = subject.Id, SemesterId = semester.Id };
                            context.SemesterSubjects.Add(t);
                            semester.SemesterSubjects.Add(t);
                        }
                        foreach (var electiveGroup in model.Semesters[i].Electives)
                        {
                            var t = new SemesterElectiveGroup { ElectiveGroupId = electiveGroup.Id, SemesterId = semester.Id };
                            context.SemesterElectiveGroups.Add(t);
                            semester.SemesterElectiveGroups.Add(t);
                        }
                        ryp.Semesters.Add(semester);
                    }

                    context.SaveChanges();
                    tran.Commit();
                }
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
                    var ryp = await context.Ryps.FirstOrDefaultAsync(x => x.Id == id);
                    ryp.Deleted = 1;
                    await context.SaveChangesAsync();
                    tran.Commit();
                }
            }
            return Ok();
        }
    }
}
