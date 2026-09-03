using LibraryManagementSystem.Dtos.User;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService= userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get_Users(int page=1, int pagesize = 5)
        {
            var get_users = await _userService.Get_All_Users(page, pagesize);
            return Ok(get_users);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get_Users_Id(int id)
        {
            var get_users_id = await _userService.Get_by_id(id);
            return Ok(get_users_id);
        }

        [HttpPost]
        public async Task<IActionResult> Add_Users([FromBody] CreateUserDto create)
        {
            var add_users = await _userService.Create_Users(create);
            return CreatedAtAction(
                nameof(Get_Users_Id),
                new { id = add_users.Id },
                add_users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUsersDto update)
        {
            var update_users = await _userService.Update_User(id, update);
            if (!update_users)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete_users = await _userService.Delete_User(id);
            if (!delete_users)
            {
                return NotFound();
            }
            return NoContent(); 
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Searching(int page = 1, int pagesize = 5, string? field = null, string? order="asc", string? search=null, string? username=null, string? roles=null)
        {
            var search_user = await _userService.Search(page, pagesize, field, order, search, username, roles);
            return Ok(search_user);
        }
    }
}
