namespace LibraryManagementSystem.Dtos.Book
{
    public class UpdateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public int ISBN { get; set; }
        public DateOnly PublishedYear { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
