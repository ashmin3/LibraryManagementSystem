using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateOnly PublishedYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int CategorysId { get; set; }
    }
}
