using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDoListAppAPI.Services.Interfaces;
using ToDoListAppData.Model;

namespace ToDoListAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        IConfiguration _configuration;
        IRoleService _roleService;
        UserManager<User> _userManager;
        public RolesController(
            IRoleService roleService,
            RoleManager<Role> roleManager,
            IConfiguration configuration
            )
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        // GET: api/Roles
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Roles/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<IActionResult> Post(Role model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role();
                role.Id = model.Id;
                role.Name = model.Name;
                role.CreateDate = DateTimeOffset.Now.ToLocalTime();
                var check = await _roleManager.RoleExistsAsync(model.Name);
                if (check == true)
                {
                    return BadRequest(model.Name + " role already exists");
                }
                else
                {
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
