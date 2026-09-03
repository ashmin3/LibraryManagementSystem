using LibraryManagementSystem.Dtos.MemberProfile;
using LibraryManagementSystem.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberProfileController : ControllerBase
    {
        private readonly IMemberProfileServices  _memberProfileServices;
        public MemberProfileController(IMemberProfileServices memberProfileServices)
        {
            _memberProfileServices= memberProfileServices;
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet]
        public async Task<IActionResult> Get_All(int page=1, int pagesize = 5)
        {
            var member_profile = await _memberProfileServices.get_All_MP(page, pagesize);
            return Ok(member_profile);
        }

        [Authorize(Roles = "Admin,Librarian,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            var member_profile = await _memberProfileServices.get_by_id_mp(id);
            return Ok(member_profile);
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]
        public async Task<IActionResult> Create_MP([FromBody] CreateMemberProfileDto created)
        {
            var create_mp = await _memberProfileServices.Create_Member_Profile(created);
            return CreatedAtAction(nameof(Get_By_Id),
                new { id = create_mp.Id },
                create_mp);
        }


        [Authorize(Roles = "Admin,Librarian")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberProfileDto updated)
        {
            var update_mp = await _memberProfileServices.Update_Member_Profile(id, updated);
            if (!update_mp)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete_mp = await _memberProfileServices.Delete_Member_Profile(id);
            if (!delete_mp)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin,Librarian")]
        [HttpGet("Search")]
        public async Task<IActionResult> Searching(int page=1, int pagesize=5, string? field=null, string? order="asc", string? search=null, int? id=null, string? phone=null, string? address=null)
        {
            var search_member_profile = await _memberProfileServices.Search(page, pagesize, field, order, search, id, phone, address);
            return Ok(search_member_profile);
        }

    }
}
