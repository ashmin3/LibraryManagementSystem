using LibraryManagementSystem.Dtos.MemberProfile;

namespace LibraryManagementSystem.Interface
{
    public interface IMemberProfileServices
    {
        Task<List<MemberProfileDto>> get_All_MP(int page, int pagesize);
        Task<MemberProfileDto?> get_by_id_mp(int id);
        Task<MemberProfileDto> Create_Member_Profile(CreateMemberProfileDto created);
        Task<bool> Update_Member_Profile(int id, UpdateMemberProfileDto updated);
        Task<bool> Delete_Member_Profile(int id);
        Task<List<MemberProfileDto>> Search(int page, int pagesize, string? field, string? order, string? search, int? id, string? phone, string? address);
    }
}
