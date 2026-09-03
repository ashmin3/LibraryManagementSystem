using LibraryManagementSystem.Dtos.AuthorBook;

namespace LibraryManagementSystem.Interface
{
    public interface IAuthorBookService
    {
        Task<List<AuthorBookDto>> Get_All_Author_Book(int page, int pagesize);
        Task<AuthorBookDto?> Get_By_Id(int id);
        Task<AuthorBookDtoint> Create_Author_Book(CreateAuthorBookDto created);
        Task<bool> Update_Author_Book(int id, UpdateAuthorBookDto updated);
        Task<bool> Delete_Author_Book(int id);
        Task<List<AuthorBookDto>> Search(int page, int pagesize, string? field, string? order, string? search, string? authorsname, string? booksname);
    }
}
