using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Bearer")]
    public class ElectiveGroupsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            List<ElectiveGroupVM> result = new List<ElectiveGroupVM>();
            using (var ctx = new RypDbContext())
            {
                result = ctx.ElectiveGroups
                            .Include(x => x.SubjectElectiveGroups)
                            .Select(x => new ElectiveGroupVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Shifr = x.Shifr,
                    Credits = x.Credits,
                    Pr = x.Pr,
                    UserId = x.UserId,
                    Date = x.Date,
                    UpdateDate = x.UpdateDate,
                    Type = x.Type,
                    Deleted = x.Deleted,
                    Subjects = x.SubjectElectiveGroups.Select(y => new SubjectVM
                    {
                        Id = y.Subject.Id,
                        Name = y.Subject.Name,
                        Shifr = y.Subject.Shifr,
                        Credits = y.Subject.Credits,
                        Lec = y.Subject.Lec,
                        Lab = y.Subject.Lab,
                        Pr = y.Subject.Pr,
                        UserId = y.Subject.UserId,
                        Date = y.Subject.Date,
                        UpdateDate = y.Subject.UpdateDate,
                        Type = y.Subject.Type,
                        Prerequisites = y.Subject.RelatedItems.Select(z => new SubjectVM
                        {
                            Id = z.RelatedId,
                            Name = z.Related.Name,
                            Shifr = z.Related.Shifr,
                            Credits = z.Related.Credits,
                            Lec = z.Related.Lec,
                            Lab = z.Related.Lab,
                            Pr = z.Related.Pr,
                            UserId = z.Related.UserId,
                            Date = z.Related.Date,
                            UpdateDate = z.Related.UpdateDate,
                            Type = z.Related.Type
                        }).ToList()
                    }).ToList()
                }).Where(x => x.Deleted == 0).ToList();
            }

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ElectiveGroupVM model)
        {
            using (var ctx = new RypDbContext())
            {
                using (var tran = ctx.Database.BeginTransaction())
                {
                    var electiveGroup = new ElectiveGroup();
                    electiveGroup.Name = model.Name;
                    electiveGroup.Shifr = model.Shifr;
                    electiveGroup.Credits = model.Credits;
                    electiveGroup.Pr = model.Pr;
                    electiveGroup.UserId = model.UserId;
                    electiveGroup.Date = DateTime.Now.ToString();
                    electiveGroup.TypeId = model.Type.Id;

                    ctx.ElectiveGroups.Add(electiveGroup);              
                    ctx.SaveChanges();

                    foreach (var item in model.Subjects)
                    {
                        var t = new SubjectElectiveGroup { ElectiveGroupId = electiveGroup.Id, SubjectId = item.Id };
                        electiveGroup.SubjectElectiveGroups.Add(t);
                    }

                    await ctx.SaveChangesAsync();
                    tran.Commit();
                }
            }
        
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ElectiveGroupVM model)
        {
            using (var context = new RypDbContext())
            {
                var electiveGroup = context.ElectiveGroups.FirstOrDefault(x => x.Id == id);
                var subjects = context.SubjectElectiveGroups.Where(x => x.ElectiveGroupId == electiveGroup.Id).ToList();
                electiveGroup.SubjectElectiveGroups = subjects;
                electiveGroup.Name = model.Name;
                electiveGroup.Shifr = model.Shifr;
                electiveGroup.Credits = model.Credits;
                electiveGroup.Pr = model.Pr;
                electiveGroup.UpdateDate = DateTime.Now.ToString();
                electiveGroup.TypeId = model.Type.Id;

                for (int i = electiveGroup.SubjectElectiveGroups.Count; i > 0; i--)
                {
                    context.SubjectElectiveGroups.Remove(electiveGroup.SubjectElectiveGroups[i - 1]);
                }
                context.SaveChanges();
                electiveGroup.SubjectElectiveGroups = new List<SubjectElectiveGroup>();

                for (int i = 0; i < model.Subjects.Count; i++)
                {
                    electiveGroup.SubjectElectiveGroups.Add(new SubjectElectiveGroup { ElectiveGroupId = electiveGroup.Id, SubjectId = model.Subjects[i].Id });
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
                    var electiveGroup = await context.ElectiveGroups.FirstOrDefaultAsync(x => x.Id == id);
                    electiveGroup.Deleted = 1;
                    await context.SaveChangesAsync();
                    tran.Commit();
                }
            }
            return Ok();
        }
    }
}
