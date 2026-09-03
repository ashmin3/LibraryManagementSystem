using AutoMapper;
using LibraryManagementSystem.Dtos.User;
using LibraryManagementSystem.Models.User;

namespace LibraryManagementSystem.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Users, UserDto>();
            CreateMap<CreateUserDto, Users>();
            CreateMap<UpdateUsersDto, Users>();
        }
    }
}
