using LibraryManagementSystem.Dtos.Member;

namespace LibraryManagementSystem.Interface
{
    public interface IMemberService
    {
        Task<List<MemberDto>> Get_All(int page, int pagesize);
        Task<MemberDto?> Get_By_Id(int id);
        Task<MemberDto> Create_Member(CreateMemberDto create);
        Task<bool> Update(int id, UpdateMemberDto updated);
        Task<bool> Delete(int id);
        Task<List<MemberDto>> Search(int page, int pagesize, string? field, string? order, string? search, int? id, string? name, string? email);
    }
}
