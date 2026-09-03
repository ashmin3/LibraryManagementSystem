using LibraryManagementSystem.Dtos.AuthorBook;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {
        private readonly IAuthorBookService _authorBookService;
        public AuthorBookController(IAuthorBookService authorBookService)
        {
            _authorBookService = authorBookService;
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet]
        public async Task<IActionResult> Get_All(int page = 1, int pagesize = 5)
        {
            var author_book = await _authorBookService.Get_All_Author_Book(page, pagesize);
            return Ok(author_book);
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get_Id(int id)
        {
            var author_book_id = await _authorBookService.Get_By_Id(id);
            return Ok(author_book_id);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Create_AuthorBook(CreateAuthorBookDto created)
        {
            var create_author_book = await _authorBookService.Create_Author_Book(created);

            return CreatedAtAction(nameof(Get_Id),
                new { id = create_author_book.Id },
                create_author_book);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update_AuthorBook(int id, UpdateAuthorBookDto updated)
        {
            var update_Author_book = await _authorBookService.Update_Author_Book(id, updated);
            if (!update_Author_book)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete_AuhtorBook(int id)
        {
            var delete_author_book = await _authorBookService.Delete_Author_Book(id);
            if (!delete_author_book)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("Search")]
        public async Task<IActionResult> Searching(int page=1, int pagesize=5, string? field=null, string? order="asc", string? search=null, string? authorsname=null, string? booksname=null)
        {
            var search_Author = await _authorBookService.Search(page, pagesize, field, order, search, authorsname, booksname);
            return Ok(search_Author);
        }

    }
}
