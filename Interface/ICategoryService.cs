using LibraryManagementSystem.Dtos.Category;

namespace LibraryManagementSystem.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> Get_All_Category(int page, int pagesize);
        Task<CategoryDto> Get_Id_Category(int id);
        Task<CategoryDto> Create_Category(CreateCategoryDto created);
        Task<bool> Update_Category(int id, UpdateCategoryDto updated);
        Task<bool> Delete_Category(int id);
        Task<List<CategoryDto>> Search(int page, int pagesize, string? field, string? order, string? search, int? id, string? name, string? description);
    }
}
