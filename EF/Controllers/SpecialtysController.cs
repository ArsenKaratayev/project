using System;
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
    public class SpecialtysController : Controller
    {
        private readonly RypDbContext _context;
        public SpecialtysController()
        {
            _context = new RypDbContext();
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            List<Specialty> result = null;
            using (var ctx = new RypDbContext())
            {
                result = ctx.Specialties.Select(x => new Specialty
                {
                    Id = x.Id,
                    Name = x.Name,
                    Shifr = x.Shifr,
                    UserId = x.UserId,
                    Date = x.Date,
                    UpdateDate = x.UpdateDate,
                    Deleted = x.Deleted
                }).Where(x => x.Deleted == 0).ToList();
            }
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult>Post([FromBody]SpecialtyVM model)
        {
            var specialty = new Specialty();
            using (var context = new RypDbContext())
            {
                specialty.Name = model.Name;
                specialty.Shifr = model.Shifr;
                specialty.UserId = model.UserId;
                specialty.Date = DateTime.Now.ToString();

                var result = await context.Specialties.AddAsync(specialty);
                context.SaveChanges();
            }
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]SpecialtyVM model)
        {
            using (var context = new RypDbContext())
            {
                var specialty = context.Specialties.FirstOrDefault(x => x.Id == id);
                specialty.Name = model.Name;
                specialty.Shifr = model.Shifr;
                specialty.UpdateDate = DateTime.Now.ToString();

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
                    var specialty = await context.Specialties.FirstOrDefaultAsync(x => x.Id == id);
                    specialty.Deleted = 1;
                    await context.SaveChangesAsync();
                    tran.Commit();
                }
            }
            return Ok();
        }
    }
}
