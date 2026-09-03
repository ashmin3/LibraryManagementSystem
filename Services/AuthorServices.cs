using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Author;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.Author;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class AuthorServices :IAuthorService
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<AuthorServices> _logger;
        private readonly IMapper _mapper;
        public AuthorServices(LibraryDbContext libraryDbContext,ILogger<AuthorServices> logger,IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<AuthorDto>> Get_All_Author(int page, int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var author = await _libraryDbContext.Authors.AsNoTracking().Skip(skip).Take(pagesize).ToListAsync();

            var map_author = _mapper.Map<List<AuthorDto>>(author);

            _logger.LogInformation("Author Retrived Successdully");

            return map_author;
        }

        public async Task<AuthorDto?> Get_Author_Id(int id)
        {
            var author = await _libraryDbContext.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(author == null)
            {
                throw new KeyNotFoundException("Invalif author Id");
            }

            var map_author = _mapper.Map<AuthorDto>(author);

            _logger.LogInformation("Author Retrived Successdully");
            return map_author;
        }

        public async Task<AuthorDto> Create_Author(CreateAuthorDto created)
        {
            var author = _mapper.Map<Authors>(created);

            await _libraryDbContext.Authors.AddAsync(author);

            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("Author created successfully");

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<bool> Update_Author(int id, UpdateAuthorDto updated)
        {
            var author = await _libraryDbContext.Authors.FirstOrDefaultAsync(x=>x.Id == id);
            if(author == null)
            {
                return false;
            }
            _mapper.Map(updated, author);
            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("Successfully updated Authors");
            return true;
        }


        public async Task<bool> Delete_Author(int id)
        {
            var author = await _libraryDbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {
                return false;
            }

            _libraryDbContext.Authors.Remove(author);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<AuthorDto>> Search(int page, int pagesize,string? field, string? order,string? search,int? id, string? name ,string? biography)
        {
            int skip = (page - 1) * pagesize;
            IQueryable<Authors> queries = _libraryDbContext.Authors;
            field = field?.ToLower();
            order = order?.ToLower();

            if (field == "id")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Id)
                    : queries.OrderByDescending(x => x.Id);
            }
            else if(field == "name")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Name)
                    : queries.OrderByDescending(x => x.Name);
            }
            else if (field == "biography")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Biography)
                    : queries.OrderByDescending(x => x.Biography);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries = queries.Where(x => x.Id.ToString().Contains(search) ||
               x.Name.Contains(search) ||
               x.Biography.Contains(search));
            }

            if (id.HasValue)
            {
                queries = queries.Where(x => x.Id == id.Value);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                queries = queries.Where(x => x.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(biography))
            {
                queries = queries.Where(x => x.Biography == biography);
            }

            return await queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new AuthorDto 
            {
            Id=x.Id,
            Name=x.Name,
            Biography=x.Biography
            }).ToListAsync();

        }
    }
}
