using LibraryManagementSystem.Dtos.Book;

namespace LibraryManagementSystem.Interface
{
    public interface IBookService
    {
        Task<List<BookDto>> Get_All_Books(int page, int pagesize);
        Task<BookDto> Get_Books_By_Id(int id);
        Task<BookDto> Create_Book(CreateBookDto created);
        Task<bool> Update_Books(int id, UpdateBookDto update);
        Task<bool> Delete_Books(int id);
        Task<List<BookDto>> Search(int page, int pagesize,string? field, string? order, string? search, int? id, string? title);
    }
}
