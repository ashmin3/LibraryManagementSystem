using AutoMapper;
using LibraryManagementSystem.Dtos.Book;
using LibraryManagementSystem.Models.Book;

namespace LibraryManagementSystem.Mappings
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Books, BookDto>();
            CreateMap<CreateBookDto, Books>();
            CreateMap<UpdateBookDto, Books>();
        }
    }
}
