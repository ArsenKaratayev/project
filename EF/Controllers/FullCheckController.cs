using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Bearer")]
    public class FullCheckController : Controller
    {
        private readonly CheckRypContext db;

        public FullCheckController(CheckRypContext context)
        {
            db = context;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await db.GetRyps();

            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RypVM model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new RypDbContext())
                {
                    using (var tran = context.Database.BeginTransaction())
                    {
                        var ryp = await context.Ryps.FirstOrDefaultAsync(x => x.Id == model.Id);
                        ryp.FullCheck = 1;

                        await context.SaveChangesAsync();

                        var checkedRyp = new CheckedRyp();
                        checkedRyp.RypId = model.Id;
                        checkedRyp.Year = model.Year;
                        checkedRyp.Specialty = model.Specialty;
                        checkedRyp.OperatorCheck = model.OperatorCheck;
                        checkedRyp.FullCheck = model.FullCheck;
                        checkedRyp.UserId = model.UserId;
                        checkedRyp.Date = model.Date;
                        checkedRyp.UpdateDate = model.UpdateDate;
                        checkedRyp.Semesters = model.Semesters;
                        checkedRyp.isOpen = 0;
                        await db.Create(checkedRyp);

                        tran.Commit();
                    }
                }
            }
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CheckedRyp model)
        {
            using (var context = new RypDbContext())
            {
                var ryp = await context.Ryps.FirstOrDefaultAsync(x => x.Id == id);
                ryp.FullCheck = 0;

                await context.SaveChangesAsync();

                var checkedRyp = await db.GetRyp(model.Id);
                checkedRyp.isOpen = 1;
                await db.Update(checkedRyp);
            }
            return Ok();
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{

        //}
    }
}
