using LibraryManagementSystem.Models.Author;
using LibraryManagementSystem.Models.Book;

namespace LibraryManagementSystem.Models.AuthorBook
{
    public class AuthorBooks
    {
        public int Id { get; set; }
        public int AuthorsId { get; set; }
        public Authors? Authors { get; set; }
        public int BooksId { get; set; }
        public Books? Books { get; set; }
    }
}
