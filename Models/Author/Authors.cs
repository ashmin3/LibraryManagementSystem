using LibraryManagementSystem.Models.AuthorBook;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Author
{
    public class Authors
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [StringLength(100,MinimumLength =3)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Field Required")]
        [StringLength(10000, MinimumLength = 3)]
        public string Biography { get; set; } = string.Empty;

        public ICollection<AuthorBooks> AuthorBooks { get; set; } = new List<AuthorBooks>();
       
    }
}
