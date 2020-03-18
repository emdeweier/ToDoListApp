﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDoListAppAPI.Services.Interfaces;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        IConfiguration _configuration;
        public UsersController(IUserService userService,
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _configuration = configuration;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            var getall = _userService.Get();
            if (getall == null)
            {
                return NotFound();
            }
            return Ok(getall);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var getbyid = _userService.Get(id);
            if (getbyid == null)
            {
                return NotFound();
            }
            return Ok(getbyid);
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult Post(User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User { };
                    user.Id = Guid.NewGuid().ToString();
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.PasswordHash = model.PasswordHash;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Name = model.Name;
                    user.JoinDate = DateTimeOffset.Now.ToLocalTime();
                    user.CreateDate = user.JoinDate;
                    user.SecurityStamp = _userManager.GetSecurityStampAsync(user).ToString();
                    var result = _userManager.CreateAsync(user, model.PasswordHash);
                    if (result.Result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(user, "User");
                        return Ok("Register Success");
                    }
                }
                catch
                {
                    throw;
                }
            }
            return BadRequest(ModelState);
        }

        // POST: api/Users
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserVM userVM)
        {
            var result = await _signInManager.PasswordSignInAsync(userVM.UserName, userVM.PasswordHash, false, true);
            var user = await _userManager.FindByNameAsync(userVM.UserName);
            //if (result.Result.IsLockedOut)
            //{
            //    if (user != null)
            //    {
            //        user.LockedStatus = true;
            //        _userManager.UpdateAsync(user);
            //        var statusCode = 300;
            //        var token = user.Token;
            //        return BadRequest(statusCode + "..." + token);
            //    }
            //}
            if (result.Succeeded)
            {
                //    var role = await _userManager.GetRolesAsync(user);
                //    List<int> temp = new List<int>();

                //    foreach (var i in role)
                //    {
                //        var priority = await _roleManager.FindByNameAsync(i);
                //        temp.Add(priority.Priority);
                //    }

                //    int change = 0;

                //    if (role.Count > 0)
                //    {
                //        for (int i = 0; i < role.Count() - 1; i++)
                //        {
                //            for (int j = 0; j < role.Count() - 1; j++)
                //            {
                //                if (temp[j] > temp[j + 1])
                //                {
                //                    change = temp[j + 1];
                //                    temp[j + 1] = temp[j];
                //                    temp[j] = change;
                //                    var priority = _roleManager.FindByNameAsync(role[change]);
                //                    userVM.Role_Name = priority.Name;
                //                }
                //            }
                //        }
                //    }
                //    var check = _roleManager.FindByNameAsync(role[change]);
                //    userVM.Role_Name = check.Name;
                //    var claims = new List<Claim>
                //            {
                //            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                //            };

                //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                //    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //    var token = new JwtSecurityToken(
                //        _configuration["Jwt:Issuer"],
                //        _configuration["Jwt:Audience"],
                //        claims,
                //        expires: DateTime.UtcNow.AddHours(12),
                //        signingCredentials: signIn
                //);
                //    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                //    claims.Add(new Claim("TokenSecurity", idtoken));
                //    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                return Ok(user.UserName + "..." + user.Id + "..." + user.Name);
            }
            else
            {
                var message = "Username or Password is Invalid";
                return BadRequest(user.AccessFailedCount + "..." + message);
            }
            //return NotFound();
        }

        // PUT: api/Users/5
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
