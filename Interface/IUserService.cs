using LibraryManagementSystem.Dtos.User;

namespace LibraryManagementSystem.Interface
{
    public interface IUserService
    {
        Task<List<UserDto>> Get_All_Users(int page, int pagesize);
        Task<UserDto?> Get_by_id(int id);
        Task<UserDto> Create_Users(CreateUserDto create);
        Task<bool> Update_User(int id, UpdateUsersDto update);
        Task<bool> Delete_User(int id);
        Task<List<UserDto>> Search(int page, int pagesize, string? field, string? order, string? search, string? username, string? roles);
    }
}
