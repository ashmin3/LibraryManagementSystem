using LibraryManagementSystem.Dtos.BorrowRecords;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedRecordController : ControllerBase
    {
        private readonly IBorrowRecordServices _borrowRecordServices;
        public BorrowedRecordController(IBorrowRecordServices borrowRecordServices)
        {
            _borrowRecordServices = borrowRecordServices;
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet]
        public async Task<IActionResult> Get_All(int page=1, int pagesize = 5)
        {
            var get_all_br = await _borrowRecordServices.Get_Borrrow_Record(page, pagesize);
            return Ok(get_all_br);
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get_Id(int id)
        {
            var get_all_br_id = await _borrowRecordServices.Get_Borrow_Record_Id(id);
            return Ok(get_all_br_id);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Add_Record([FromBody] CreateBorrowedRecordDto created)
        {
            var create_record = await _borrowRecordServices.Create_Records(created);
            return CreatedAtAction(nameof(Get_Id),
                new { id = create_record.Id },
                create_record);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBorrowedRecordDto updated)
        {
            var update_record = await _borrowRecordServices.Update_Record(id, updated);
            if (!update_record)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var delete_record = await _borrowRecordServices.Delete_Record(id);
            if (!delete_record)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(int page=1, int pagesize=5, string? field=null, string? order="asc", string? search=null, int? id=null, string? status=null)
        {
            var borrow_record = await _borrowRecordServices.Search(page, pagesize, field, order, search, id, status);
            return Ok(borrow_record);
        }
    }
}
