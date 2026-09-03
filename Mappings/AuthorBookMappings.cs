using AutoMapper;
using LibraryManagementSystem.Dtos.AuthorBook;
using LibraryManagementSystem.Models.Author;
using LibraryManagementSystem.Models.AuthorBook;

namespace LibraryManagementSystem.Mappings
{
    public class AuthorBookMappings :Profile
    {
        public AuthorBookMappings()
        {
            CreateMap<AuthorBooks, AuthorBookDto>();
            CreateMap<AuthorBooks, AuthorBookDtoint>();
            CreateMap<CreateAuthorBookDto, AuthorBooks>();
            CreateMap<UpdateAuthorBookDto, AuthorBooks>();
        }
    }
}
