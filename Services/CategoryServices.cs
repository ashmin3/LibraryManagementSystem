using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Category;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class CategoryServices :ICategoryService
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<CategoryServices> _logger;
        private readonly IMapper _mapper;
        public CategoryServices(LibraryDbContext libraryDbContext, ILogger<CategoryServices> logger, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Get_All_Category(int page, int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var category = await _libraryDbContext.Categories.AsNoTracking().Skip(skip).Take(pagesize).ToListAsync();

            var map_category = _mapper.Map<List<CategoryDto>>(category);

            _logger.LogInformation("Category retrived successfully");

            return map_category;
        }

        public async Task<CategoryDto> Get_Id_Category(int id)
        {
            var category = await _libraryDbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                throw new KeyNotFoundException("Invalid Id ");
            }

            var map_category = _mapper.Map<CategoryDto>(category);

            _logger.LogInformation("Category retrived successfully");

            return map_category;
        }

        public async Task<CategoryDto> Create_Category(CreateCategoryDto created)
        {
            var category = _mapper.Map<Categorys>(created);

            await _libraryDbContext.Categories.AddAsync(category);
            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("Category created successfully");

            return _mapper.Map<CategoryDto>(category);

        }

        public async Task<bool> Update_Category(int id, UpdateCategoryDto updated)
        {
            var category = await _libraryDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return false;
            }

            _mapper.Map(updated, category);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        } 

        public async Task<bool> Delete_Category(int id)
        {
            var category = await _libraryDbContext.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            
            if (category == null)
            {
                return false;
            }

            _libraryDbContext.Categories.Remove(category);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<CategoryDto>> Search(int page,int pagesize, string? field,string? order,string? search,int? id, string? name , string? description )
        {
            int skip = (page - 1) * pagesize;

            IQueryable<Categorys> queries = _libraryDbContext.Categories;
            field = field?.ToLower();
            order = order?.ToLower();

            if (field == "id")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Id )
                    : queries.OrderByDescending(x => x.Id);
            }

            else if(field == "name")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Name)
                    : queries.OrderByDescending(x => x.Name);
            }
            else if (field == "description")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Description)
                    : queries.OrderByDescending(x => x.Description);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries = queries.Where(
                    x => x.Id.ToString().Contains(search) ||
                    x.Name.Contains(search) ||
                    x.Description.Contains(search)
                );
            }

            if (id.HasValue)
            {
                queries = queries.Where(x => x.Id == id.Value);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                queries = queries.Where(x => x.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                queries = queries.Where(x => x.Description == description);
            }

            return await queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }
    }
}
