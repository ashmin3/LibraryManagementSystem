using LibraryManagementSystem.Models.Member;

namespace LibraryManagementSystem.Models.MemberProfile
{
    public class MemberProfiles
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Phone { get; set; } = string.Empty;
        public DateOnly MembershipDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);



        // navigational + conventional properties
        public int MemberId { get; set; }
        public Members? Members { get; set;}
    }
}
