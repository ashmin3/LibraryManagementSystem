using AutoMapper;
using LibraryManagementSystem.Dtos.MemberProfile;
using LibraryManagementSystem.Models.MemberProfile;

namespace LibraryManagementSystem.Mappings
{
    public class MemberProfileMappings :Profile
    {
        public MemberProfileMappings()
        {
            CreateMap<MemberProfiles, MemberProfileDto>();
            CreateMap<CreateMemberProfileDto, MemberProfiles>();
            CreateMap<UpdateMemberProfileDto, MemberProfiles>();
        }
    }
}
