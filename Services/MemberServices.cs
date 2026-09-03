using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Member;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagementSystem.Services
{
    public class MemberServices :IMemberService
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<MemberServices> _logger;
        private readonly IMapper _mapper;
        public MemberServices(LibraryDbContext libraryDbContext, ILogger<MemberServices> logger ,IMapper mapper)
        {
            _libraryDbContext= libraryDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<MemberDto>> Get_All(int page, int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var member = await _libraryDbContext.Members.AsNoTracking().Skip(skip).Take(pagesize).ToListAsync();

            var member_mapped = _mapper.Map<List<MemberDto>>(member);

            _logger.LogInformation("Students retrived Successfully");

            return member_mapped;
        }

        public async Task<MemberDto?> Get_By_Id(int id)
        {
            var member = await _libraryDbContext.Members.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(member == null)
            {
                throw new Exception("Invalid Id Found");
            }

            var member_mapped = _mapper.Map<MemberDto>(member);

            _logger.LogInformation("Member retrived");

            return member_mapped;
        }

        public async Task<MemberDto> Create_Member(CreateMemberDto create)
        {
            var member = _mapper.Map<Members>(create);

            await _libraryDbContext.Members.AddAsync(member);

            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("Member created Successfully");

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<bool> Update(int id ,UpdateMemberDto updated)
        {
            var member = await _libraryDbContext.Members.FirstOrDefaultAsync(x => x.Id == id);

            if(member == null)
            {
                return false;
            }

            _mapper.Map(updated, member);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var member = await _libraryDbContext.Members.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (member == null)
            {
                return false;
            }

            _libraryDbContext.Members.Remove(member);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<MemberDto>> Search(int page, int pagesize, string? field , string? order,string? search,int? id, string? name, string? email)
        {
            int skip = (page - 1) * pagesize;

            IQueryable <Members> queries= _libraryDbContext.Members;
            field = field?.ToLower();
            order = order?.ToLower();

            if(field == "id")
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
            else if(field =="email")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Email)
                    : queries.OrderByDescending(x => x.Email);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries = queries.Where(
                    x => x.Id.ToString().Contains(search) ||
                    x.Name.Contains(search) ||
                    x.Email.Contains(search)
                );
            }

            if (id.HasValue)
            {
                queries = queries.Where(x => x.Id == id);
            }
            if (!string.IsNullOrWhiteSpace(name)){
                queries = queries.Where(x => x.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                queries = queries.Where(x => x.Email == email);
            }

            return await queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new MemberDto
            {
                Id=x.Id,
                Name=x.Name,
                Email=x.Email
            }).ToListAsync();

        }

    }
}
