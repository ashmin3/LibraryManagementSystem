using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.BorrowRecords;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.BorrowRecord;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services
{
    public class BorrowedRecordServices : IBorrowRecordServices
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly ILogger<BorrowedRecordServices> _logger;
        private readonly IMapper _mapper;
        public BorrowedRecordServices(LibraryDbContext libraryDbContext,ILogger<BorrowedRecordServices> logger, IMapper mapper)
        {
            _libraryDbContext= libraryDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<BorrowRecordDto>> Get_Borrrow_Record(int page, int pagesize)
        {
            int skip = (page - 1) * pagesize;

            var borrow_record = await _libraryDbContext.BorrowedRecords.AsNoTracking().Skip(skip).Take(pagesize).ToListAsync();

            var map_br = _mapper.Map<List<BorrowRecordDto>>(borrow_record);

            _logger.LogInformation("Book Borrewed Record Successfully received");

            return map_br;
        }

        public async Task<BorrowRecordDto> Get_Borrow_Record_Id(int id)
        {
            var borrow_record = await _libraryDbContext.BorrowedRecords.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(borrow_record == null)
            {
                throw new Exception("Invalid Borrow Record Found with id : "+ id);
            }

            var map_br = _mapper.Map<BorrowRecordDto>(borrow_record);

            _logger.LogInformation("Successfully retrived Data with ID"+ id);

            return map_br;
        }

        public async Task<BorrowRecordDto> Create_Records (CreateBorrowedRecordDto created)
        {
            var borrow_record = _mapper.Map<BorrowedRecords>(created);

            await _libraryDbContext.BorrowedRecords.AddAsync(borrow_record);

            await _libraryDbContext.SaveChangesAsync();

            _logger.LogInformation("Record Created Successfully");

            return _mapper.Map<BorrowRecordDto>(borrow_record);
        }

        public async Task<bool> Update_Record(int id,UpdateBorrowedRecordDto  updated )
        {
            var borrowed_records = await _libraryDbContext.BorrowedRecords.FirstOrDefaultAsync(x => x.Id == id);

            if (borrowed_records == null)
            {
                return false;
            }

            _mapper.Map(updated, borrowed_records);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete_Record(int id)
        {
            var borrowed_records = await _libraryDbContext.BorrowedRecords.FirstOrDefaultAsync(x => x.Id == id);

            if (borrowed_records == null)
            {
                return false;
            }

            _libraryDbContext.BorrowedRecords.Remove(borrowed_records);
            await _libraryDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<BorrowRecordDto>> Search(int page, int pagesize, string? field, string? order,string? search , int? id, string? status)
        {
            int skip = (page - 1) * pagesize;
            field = field?.ToLower();
            order = order?.ToLower();

            IQueryable<BorrowedRecords> queries = _libraryDbContext.BorrowedRecords;

            if (field == "id")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Id)
                    : queries.OrderByDescending(x => x.Id);
            }
            else if (field == "status")
            {
                queries = order == "asc"
                    ? queries.OrderBy(x => x.Status)
                    : queries.OrderByDescending(x => x.Status);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                queries = queries.Where(x => 
                x.Id.ToString().Contains(search) || 
                x.BorrowDate.ToString().Contains(search));
            }

            if (id.HasValue)
            {
                queries = queries.Where(x => x.Id == id);
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                queries = queries.Where(x => x.Status == status);
            }

            return await queries.AsNoTracking().Skip(skip).Take(pagesize).Select(x => new BorrowRecordDto
            {
                Id=x.Id,
                BorrowDate=x.BorrowDate,
                ReturnDate=x.ReturnDate,
                Status=x.Status,
                DueDate=x.DueDate
            }).ToListAsync();

        } 

    }
}
