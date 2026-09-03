using AutoMapper;
using LibraryManagementSystem.Dtos.Author;
using LibraryManagementSystem.Models.Author;

namespace LibraryManagementSystem.Mappings
{
    public class AuthorMapping :Profile
    {
        public AuthorMapping()
        {
            CreateMap<Authors, AuthorDto>();
            CreateMap<CreateAuthorDto, Authors>();
            CreateMap<UpdateAuthorDto, Authors>();
        }
    }
}
