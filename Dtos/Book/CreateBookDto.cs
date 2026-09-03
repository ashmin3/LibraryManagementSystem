namespace LibraryManagementSystem.Dtos.Book
{
    public class CreateBookDto
    {

        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateOnly PublishedYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int CategorysId { get; set; }
    }
}
