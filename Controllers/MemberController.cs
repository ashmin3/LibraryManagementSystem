using LibraryManagementSystem.Dtos.Member;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [Authorize(Roles ="Admin,Librarian")]
        [HttpGet]
        public async Task<IActionResult> Get_ALl_Member(int page=1,int pagesize=5)
        {
            var member = await _memberService.Get_All(page, pagesize);
            return Ok(member);
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get_By_id(int id)
        {
            var member = await _memberService.Get_By_Id(id);
            return Ok(member);
        }
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Add_Member([FromBody] CreateMemberDto create)
        {
            var create_member = await _memberService.Create_Member(create);
            return CreatedAtAction(nameof(Get_By_id),
                new { id = create_member.Id },
                create_member);
        }
        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update_Member(int id, [FromBody] UpdateMemberDto updated)
        {
            var update_member = await _memberService.Update(id, updated);
            if (!update_member)
            {
                return NotFound();
            }

            return NoContent();
        }
        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var delete_member = await _memberService.Delete(id);
            if (!delete_member)
            {
                return NotFound();
            }

            return NoContent();
        }
        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("Search")]
        public async Task<IActionResult> Searching(int page=1, int pagesize=5, string? order="asc", string? field=null, string? search=null, int? id=null, string? name=null, string? email=null)
        {
            var search_member = await _memberService.Search(page, pagesize, field, order, search, id, name, email);
            return Ok(search_member);
        }
    }
}
