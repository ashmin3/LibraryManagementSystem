using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.MemberProfile;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.MemberProfile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class MemberProfileServices  : IMemberProfileServices
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<MemberProfileServices> _logger;
        private readonly IMapper _mapper;
        public MemberProfileServices(LibraryDbContext libraryDbContext, ILogger<MemberProfileServices> logger, IMapper mapper)
        {
            _libraryDbContext= libraryDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<MemberProfileDto>> get_All_MP(int page, int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var Member_Profile = await _libraryDbContext.MemberProfiles.AsNoTracking().Skip(skip).Take(pagesize).ToListAsync();

            var map_mp = _mapper.Map<List<MemberProfileDto>>(Member_Profile);

            _logger.LogInformation("Successfully retrived Member Profiles");

            return map_mp;
        }

        public async Task<MemberProfileDto?> get_by_id_mp(int id)
        {
            var Member_Profile = await _libraryDbContext.MemberProfiles.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);

            if (Member_Profile == null)
            {
                throw new KeyNotFoundException("Invalid Id ");
            }

            var map_mp = _mapper.Map<MemberProfileDto>(Member_Profile);

            _logger.LogInformation("Successfully retrived Member Profiles");

            return map_mp;
        } 


        public async Task<MemberProfileDto> Create_Member_Profile(CreateMemberProfileDto created)
        {
            var member_profile = _mapper.Map<MemberProfiles>(created);

            await _libraryDbContext.MemberProfiles.AddAsync(member_profile);

            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("Member_Profile successfully created ");

            return _mapper.Map<MemberProfileDto>(member_profile);
        }

        public async Task<bool> Update_Member_Profile(int id, UpdateMemberProfileDto updated)
        {
            var member_profile = await _libraryDbContext.MemberProfiles.FirstOrDefaultAsync(x => x.Id == id);

            if (member_profile == null)
            {
                throw new KeyNotFoundException("Invalid Id found");
            }

            _mapper.Map(updated, member_profile);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }



        public async Task<bool> Delete_Member_Profile(int id)
        {
            var member_profile = await _libraryDbContext.MemberProfiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (member_profile == null)
            {
                throw new KeyNotFoundException("Invalid Id found");
            }

            _libraryDbContext.MemberProfiles.Remove(member_profile);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<MemberProfileDto>> Search(int page ,int pagesize, string? field, string? order, string? search, int? id, string? phone , string? address)
        {
            int skip = (page - 1) * pagesize;

            IQueryable<MemberProfiles> queries = _libraryDbContext.MemberProfiles;
            field = field?.ToLower();
            order = order?.ToLower();

            if (field == "id")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Id )
                    : queries.OrderByDescending(x => x.Id );
            }

            else if(field == "address")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Address)
                    : queries.OrderByDescending(x => x.Address);

            }

            else if (field == "phone")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Phone)
                    : queries.OrderByDescending(x => x.Phone);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries = queries.Where(x => 
                x.Id.ToString().Contains(search) || 
                x.Phone.Contains(search) || 
                x.Address.Contains(search));
            }

            if (id.HasValue)
            {
                queries = queries.Where(x => x.Id == id);
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                queries = queries.Where(x => x.Phone == phone);
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                queries = queries.Where(x => x.Address == address);
            }

            return await queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new MemberProfileDto
            {
                Id=x.Id,
                Address=x.Address,
                DateOfBirth=x.DateOfBirth,
                Phone=x.Phone,
                MembershipDate=x.MembershipDate
            }).ToListAsync();

        }



    }
}
