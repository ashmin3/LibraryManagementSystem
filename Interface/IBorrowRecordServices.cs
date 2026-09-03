using LibraryManagementSystem.Dtos.BorrowRecords;

namespace LibraryManagementSystem.Interface
{
    public interface IBorrowRecordServices
    {
        Task<List<BorrowRecordDto>> Get_Borrrow_Record(int page, int pagesize);
        Task<BorrowRecordDto> Get_Borrow_Record_Id(int id);
        Task<BorrowRecordDto> Create_Records(CreateBorrowedRecordDto created);
        Task<bool> Update_Record(int id, UpdateBorrowedRecordDto updated);
        Task<bool> Delete_Record(int id);
        Task<List<BorrowRecordDto>> Search(int page, int pagesize, string? field, string? order, string? search, int? id, string? status);
    }
}
