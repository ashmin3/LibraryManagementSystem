using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.User
{
    public class Users
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Field Required")]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Required")]
        [StringLength(100, MinimumLength = 11)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Required")]
        [StringLength(100, MinimumLength = 11)]
        public string Password { get; set; } = string.Empty;

        public string Roles { get; set; } = string.Empty;
    }
}
