using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LisDo.API.Models;
using LisDo.API.Models.Dtos;
using LisDo.API.Repositories.ListItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LisDo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LisdoItemsController : ControllerBase
    {
        private readonly ILisdoItemsRepo repo;
        private readonly UserManager<User> userManager;

        public LisdoItemsController(ILisdoItemsRepo repo, UserManager<User> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string q, int lisdoId)
        {
            string uId = await GetCurrentUserId();
            var result = await repo.SearchItem(q, uId, lisdoId);

            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddItems(List<ItemCreateDto> lisdoItems)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string uId = await GetCurrentUserId();
            var result = await repo.AddItems(lisdoItems, uId);

            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(ItemDto item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string uId = await GetCurrentUserId();
            var result = await repo.UpdateItem(item, uId);

            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int itemId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string uId = await GetCurrentUserId();
            var result = await repo.DeleteItem(itemId, uId);

            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [Route("do")]
        public async Task<IActionResult> DoItem(int itemId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string uId = await GetCurrentUserId();
            var result = await repo.DoItem(itemId, uId);            

            return Ok(result);
        }

        [HttpPost]
        [Route("undo")]
        public async Task<IActionResult> UndoItem(int itemId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string uId = await GetCurrentUserId();
            var result = await repo.UndoItem(itemId, uId);

            return Ok(result);
        }

        private async Task<string> GetCurrentUserId()
        {
            User usr = await GetCurrentUserAsync();
            return usr?.Id;
        }
        [NonAction]
        private Task<User> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

    }
}