using LibraryManagementSystem.Dtos.Category;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet]
        public async Task<IActionResult> Get_All(int page=1, int pagesize = 5)
        {
            var category = await _categoryService.Get_All_Category(page, pagesize);
            return Ok(category);
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            var category_id = await _categoryService.Get_Id_Category(id);
            return Ok(category_id);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Add_Category([FromBody] CreateCategoryDto created)
        {
            var create_Category = await _categoryService.Create_Category(created);
            return CreatedAtAction(nameof(Get_By_Id),
                new { id = create_Category.Id },
                create_Category);
        }


        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto updated)
        {
            var update_Category = await _categoryService.Update_Category(id, updated);
            if (!update_Category)
            {
                return NotFound();
            }

            return NoContent();
        }


        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var delete_category = await _categoryService.Delete_Category(id);
            if (!delete_category)
            {
                return NotFound();
            }

            return NoContent();
        }


        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("Search")]
        public async Task<IActionResult> Searching(int page=1, int pagesize=5, string? field=null, string? order="asc", string? search=null, int? id=null, string? name=null, string? description=null)
        {
            var search_category = await _categoryService.Search(page, pagesize, field, order, search, id, name, description);
            return Ok(search_category);
        }
    }
}
