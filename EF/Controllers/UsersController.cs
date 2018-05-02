using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EF.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Bearer")]
    public class UsersController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly RoleManager<UserRoleEntity> _roleManager;
        private readonly IConfiguration _configuration;
        private ApplicationDbContext _context;

        public UsersController(UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            RoleManager<UserRoleEntity> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = new ApplicationDbContext();
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<UserVM> result = null;
                var context = new ApplicationDbContext();
                result = context.Users.Select(x => new UserVM
                {
                    Id = x.Id,
                    Name = x.UserName,
                    Role = _userManager.GetRolesAsync(x).Result[0]
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            UserVM result = null;

            var context = new ApplicationDbContext();
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            result = new UserVM
            {
                Id = user.Id,
                Name = user.UserName,
                Role = _userManager.GetRolesAsync(user).Result[0]
            };

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserVM model)
        {
            //if (!_roleManager.RoleExistsAsync("Admin").Result)
            //{
            //    var result = _roleManager.CreateAsync(new UserRoleEntity() {  Name = "Admin" }).Result;
            //}

            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Name not specified");
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Password not specified");
            }
            if (string.IsNullOrWhiteSpace(model.Role))
            {
                return BadRequest("Role not specified");
            }

            // Create user
            var user = new UserEntity();
            user.UserName = model.Name;
            user.Email = model.Name + "@kaznitu.kz";
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);;

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            else
            {
                return BadRequest("Unable to create user");
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        //PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                IdentityResult chanegPwdResult = await _userManager.ChangePasswordAsync(user, user.PasswordHash, model.Password);
                if (!chanegPwdResult.Succeeded)
                {
                    return BadRequest("Unable to change password");
                }

                user.UserName = model.Name;
                var roles = await _userManager.GetRolesAsync(user);

                if (roles[0] != model.Role)
                {
                    var rolesResult = await _userManager.RemoveFromRoleAsync(user, roles[0]); 
                    if (rolesResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, model.Role);
                    } 
                }
                await _context.SaveChangesAsync();

                transaction.Commit();
                return Ok();
            }           
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = _context.Roles.FirstOrDefault(x => x.Name == roles[0]);

                var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId == id && x.RoleId == role.Id);
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();

                //IdentityResult rolesResult = await _userManager.RemoveFromRoleAsync(user, roles[0]);

                //if (!rolesResult.Succeeded)
                //{
                //    transaction.Rollback();
                //    return BadRequest("Unable to remove roles from user");
                //}

                // result = await _userManager.DeleteAsync(user);

                //if (!result.Succeeded)
                //{
                //    transaction.Rollback();
                //    return BadRequest("Unable to remove user");
                //}

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                transaction.Commit();
                return Ok();
            }             
        }
    }
}
