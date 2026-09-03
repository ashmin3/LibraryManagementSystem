using LibraryManagementSystem.Dtos.Author;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet]
        public async Task<IActionResult> Get_All(int page=1,int pagesize=5)
        {
            var author = await _authorService.Get_All_Author(page, pagesize);
            return Ok(author);
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            var author = await _authorService.Get_Author_Id(id);
            return Ok(author);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Add_Author([FromBody] CreateAuthorDto created)
        {
            var create_author = await _authorService.Create_Author(created);
            return CreatedAtAction(nameof(Get_By_Id),
                new { id = create_author.Id },
                create_author);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateAuthorDto updated)
        {
            var author = await _authorService.Update_Author(id, updated);
            if (!author)
            {
                return NotFound();
            }

            return NoContent();

        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.Delete_Author(id);
            if(!author)
            {
                return NotFound();
            }
            return NoContent();       
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("Search")]
        public async Task<IActionResult> Searching(int page=1, int pagesize=5, string? field=null, string? order="asc", string? search=null, int? id=null, string? name=null, string? biography = null)
        {
            var search_Author = await _authorService.Search(page, pagesize, field, order, search, id, name, biography);
            return Ok(search_Author);
        }

    }
}
