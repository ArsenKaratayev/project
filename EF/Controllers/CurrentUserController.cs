using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Bearer")]
    public class CurrentUserController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private ApplicationDbContext _context;

        public CurrentUserController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
            _context = new ApplicationDbContext();
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            UserVM result = null;

            var context = new ApplicationDbContext();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            result = new UserVM
            {
                Id = user.Id,
                Name = user.UserName,
                Role = _userManager.GetRolesAsync(user).Result[0]
            };

            return Ok(result);
        }
    }
}
