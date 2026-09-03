using AutoMapper;
using LibraryManagementSystem.Dtos.Member;
using LibraryManagementSystem.Models.Member;

namespace LibraryManagementSystem.Mappings
{
    public class MemberMapping :Profile
    {
        public MemberMapping()
        {
            CreateMap<Members, MemberDto>();
            CreateMap<CreateMemberDto, Members>();
            CreateMap<UpdateMemberDto, Members>();
        }
    }
}
