using LibraryManagementSystem.Dtos.Author;

namespace LibraryManagementSystem.Interface
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> Get_All_Author(int page, int pagesize);
        Task<AuthorDto?> Get_Author_Id(int id);
        Task<AuthorDto> Create_Author(CreateAuthorDto created);
        Task<bool> Update_Author(int id, UpdateAuthorDto updated);
        Task<bool> Delete_Author(int id);
        Task<List<AuthorDto>> Search(int page, int pagesize, string? field, string? order, string? search, int? id, string? name, string? biography);
    }
}
