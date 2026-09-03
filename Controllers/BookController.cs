using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet]
        public async Task<IActionResult> Get_All(int page=1, int pagesize= 5)
        {
            var get_books = await _bookService.Get_All_Books(page, pagesize);
            return Ok(get_books);
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get_by_Id(int id)
        {
            var get_books_Id = await _bookService.Get_Books_By_Id(id);
            return Ok(get_books_Id);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Add_Book([FromBody] CreateBookDto created)
        {
            var create_books = await _bookService.Create_Book(created);
            return CreatedAtAction(nameof(Get_by_Id),
                new { Id = create_books.Id },
                create_books);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto update)
        {
            var update_book = await _bookService.Update_Books(id, update);
            if (!update_book)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete_book= await _bookService.Delete_Books(id);
            if (!delete_book)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("Search")]
        public async Task<IActionResult> Searching(int page=1, int pagesize=5, int? id=null, string? title=null, string? field=null, string? search=null, string? order = "asc")
        {
            var Search = await _bookService.Search(page,pagesize,field, order,search, id, title);
            return Ok(Search);
        }
    }
}   
