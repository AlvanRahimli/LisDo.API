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
    //edit role --->
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRole(CreateRole role)
        {
            if(ModelState.IsValid)
            {
                IdentityRole newRole = new IdentityRole
                {
                    Name = role.Name
                };

                IdentityResult result = await roleManager.CreateAsync(newRole);

                if (result.Succeeded)
                    return RedirectToAction("GetRoles", "Administration");

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetRoles()
        {
            var roles = roleManager.Roles;
            return Ok(roles);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound();

            var roleToReturn = new RoleDto
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                    roleToReturn.Users.Add(user.UserName);
            }

            return Ok(roleToReturn);
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> EditRole(RoleDto roleDto)
        {
            var role = await roleManager.FindByIdAsync(roleDto.RoleId);

            if (role == null)
                return NotFound();
            else
            {
                role.Name = roleDto.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("GetRoles");

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return RedirectToAction("GetRoles");
        }
    }
}