using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.User;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.User;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class UserServices : IUserService
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<UserServices> _logger;
        private readonly IMapper _mapper;
        public UserServices(LibraryDbContext libraryDbContext,ILogger<UserServices> logger, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Get_All_Users(int page, int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var users = await _libraryDbContext.Users.AsNoTracking().Skip(skip).Take(pagesize).ToListAsync();

            var map_users = _mapper.Map<List<UserDto>>(users);

            _logger.LogInformation("Retrived all the information");

            return map_users;

        }

        public async Task<UserDto?> Get_by_id(int id)
        {
            var users = await _libraryDbContext.Users.FindAsync(id);
            if(users == null)
            {
                throw new KeyNotFoundException("Invalid users found");
            }

            var map_users = _mapper.Map<UserDto>(users);
            _logger.LogInformation("Retrived the information");

            return map_users;

        }

        public async Task<UserDto> Create_Users(CreateUserDto create)
        {
            var users = _mapper.Map<Users>(create);
            users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password);

            await _libraryDbContext.AddAsync(users);
            await _libraryDbContext.SaveChangesAsync();
    
            _logger.LogInformation("Users created successfully");

            return _mapper.Map<UserDto>(users);
        }

      public async Task<bool> Update_User(int id, UpdateUsersDto update)
        {
            var users = await _libraryDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(users == null)
            {
                return false;
            }

             _mapper.Map(update, users);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete_User(int id)
        {
            var users = await _libraryDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (users == null)
            {
                return false;
            }
             _libraryDbContext.Users.Remove(users);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserDto>> Search(int page, int pagesize, string? field, string? order, string? search,string? username ,string? roles)
        {
            var skip = (page - 1) * pagesize;

            IQueryable<Users> queries = _libraryDbContext.Users;
            field = field?.ToLower();
            order = order?.ToLower();

            if(field=="id")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Id)
                    : queries.OrderByDescending(x => x.Id);
            }
            else if (field == "name")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Name)
                    : queries.OrderByDescending(x => x.Name);
            }
            else if (field == "username")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.UserName)
                    : queries.OrderByDescending(x => x.UserName);
            }
            else if(field=="roles")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Roles)
                    : queries.OrderByDescending(x => x.Roles);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries = queries.Where(x => x.Id.ToString().Contains(search) || x.Name.Contains(search) || x.UserName.Contains(search) || x.Roles.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                queries = queries.Where(x => x.UserName == username);
            }
            if (!string.IsNullOrWhiteSpace(roles))
            {
                queries = queries.Where(x => x.Roles == roles);
            }

            return await queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Roles = x.Roles
            }).ToListAsync();



        }
    }
}
