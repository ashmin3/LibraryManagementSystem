using LibraryManagementSystem.Models.AuthorBook;
using LibraryManagementSystem.Models.BorrowRecord;
using LibraryManagementSystem.Models.Category;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Book
{
    public class Books
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Field Required")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Field Required")]
        public DateOnly PublishedYear { get; set; }= DateOnly.FromDateTime(DateTime.Now);

        // navigational +conventional properties
        public int CategorysId { get; set;}
        public Categorys? Categorys { get; set; }

        public ICollection<BorrowedRecords> BorrowedRecords { get; set; } = new List<BorrowedRecords>();

        public ICollection<AuthorBooks> AuthorBooks { get; set; } = new List<AuthorBooks>();

    }
}
