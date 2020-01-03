using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LisDo.API.Models;
using LisDo.API.Repositories.Lisdos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LisDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LisdosController : ControllerBase
    {
        private readonly ILisdosRepo _repo;
        private readonly UserManager<User> userManager;

        public LisdosController(ILisdosRepo repo, UserManager<User> userManager)
        {
            _repo = repo;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("get")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicLisdos(int rn, int c)
        {
            var result = await _repo.GetLisdosFeed(rn, c);

            if (result.Count == 0)
                return NotFound("No lisdos avaliable");
            return Ok(result);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetTeamLisdos(int rn, int c, int tId)
        {
            string uId = await GetCurrentUserId();
            var result = await _repo.GetTeamLisdos(rn, c, tId, uId);

            if (result.Count == 0)
                return NotFound("No lisdos avaliable");
            return Ok(result);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetProfileLisdos(int rn, int c)
        {
            string uId = await GetCurrentUserId();
            var result = await _repo.GetProfileLisdos(rn, c, uId);

            if (result.Count == 0)
                return NotFound("No lisdos avaliable");
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]        
        public async Task<IActionResult> GetLisdo(int id)
        {
            string uId = await GetCurrentUserId();
            var result = await _repo.GetLisdo(id, uId);

            if (result == null)
                return NotFound("N/A");
            return Ok(result);
        }

        [HttpGet]
        [Route("guest/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLisdoGuest(int id)
        {
            var result = await _repo.GetLisdoGuest(id);

            if (result == null)
                return NotFound("N/A");
            return Ok(result);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateLisdo(Lisdo lisdo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string uId = await GetCurrentUserId();
            var result = await _repo.UpdateLisdo(lisdo, uId);

            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLisdo(int id)
        {
            if (id < 1)
                return BadRequest();

            string uId = await GetCurrentUserId();
            var result = await _repo.Delete(id, uId);

            if (result)
                return Ok();
            return BadRequest();
        }

        [NonAction]
        private async Task<string> GetCurrentUserId()
        {
            User usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        [NonAction]
        private Task<User> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
    }
}