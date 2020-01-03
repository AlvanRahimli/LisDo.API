using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LisDo.API.Models;
using LisDo.API.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LisDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountsController(UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            if(ModelState.IsValid)
            {
                var newUser = new User() { UserName = user.Name, Email = user.Email };
                var result = await userManager.CreateAsync(newUser, user.Password);

                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("GetPublicLisdos", "Lisdos");
                }

                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("GetPublicLisdos", "Lisdos");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager
                    .PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetPublicLisdos", "Lisdos");
                }
                
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            
            return BadRequest(ModelState);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var usr = await userManager.FindByEmailAsync(email);

            if (usr == null)
                return new JsonResult(true);
            return new JsonResult($"Email {email} is already in use");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsNameInUse(string name)
        {
            var usr = await userManager.FindByNameAsync(name);

            if (usr == null)
                return new JsonResult(true);
            return new JsonResult($"Name {name} is already in use");
        }
    }
}