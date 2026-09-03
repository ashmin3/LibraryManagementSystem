using AutoMapper;
using LibraryManagementSystem.Dtos.BorrowRecords;
using LibraryManagementSystem.Models.BorrowRecord;

namespace LibraryManagementSystem.Mappings
{
    public class BorrowedRecordMappings : Profile
    {
        public BorrowedRecordMappings()
        {
            CreateMap<BorrowedRecords, BorrowRecordDto>();
            CreateMap<CreateBorrowedRecordDto, BorrowedRecords>();
            CreateMap<UpdateBorrowedRecordDto, BorrowedRecords>();
        }
    }
}
