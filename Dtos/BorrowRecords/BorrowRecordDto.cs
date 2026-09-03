using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Dtos.BorrowRecords
{
    public class BorrowRecordDto
    {
        public int Id { get; set; }
        public DateOnly BorrowDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly ReturnDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Status { get; set; } = string.Empty;
        public int MembersId { get; set; }
        public int BooksId { get; set; }
    }
}
