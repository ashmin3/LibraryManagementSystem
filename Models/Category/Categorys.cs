using LibraryManagementSystem.Models.Book;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Category
{
    public class Categorys
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [StringLength(100,MinimumLength =3)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Field Required")]
        [StringLength(1000, MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;

        // navigational + conventional properties
        public ICollection<Books> Books { get; set; } = new List<Books>();
    }
}
