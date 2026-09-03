using LibraryManagementSystem.Models.BorrowRecord;
using LibraryManagementSystem.Models.MemberProfile;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Member
{
    public class Members
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Field Required")]
        [StringLength(100,MinimumLength =3)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Field Required")]
        [StringLength(100, MinimumLength = 11)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
       
        // navigational +conventional properties
        public MemberProfiles? MemberProfiles { get; set; }

        public ICollection<BorrowedRecords> BorrowedRecords { get; set; } = new List<BorrowedRecords>();
    }
}
