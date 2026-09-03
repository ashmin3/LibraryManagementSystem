using LibraryManagementSystem.Models.Book;
using LibraryManagementSystem.Models.Member;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.BorrowRecord
{
    public class BorrowedRecords
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Field Required")]
        public DateOnly BorrowDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
        [Required(ErrorMessage = "Field Required")]
        public DateOnly DueDate { get; set; }= DateOnly.FromDateTime(DateTime.Now);
       
        [Required(ErrorMessage = "Field Required")]
        public DateOnly ReturnDate { get; set; }= DateOnly.FromDateTime(DateTime.Now);
      
        [Required(ErrorMessage = "Field Required")]
        public string Status { get; set; } = string.Empty;

        // navigational _+conventional properties
        public int MembersId { get; set; }
        public Members? Members { get; set;}

        public int BooksId { get; set; }
        public Books? Books { get; set; }

    }
}
