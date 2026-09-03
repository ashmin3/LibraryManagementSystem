namespace LibraryManagementSystem.Dtos.MemberProfile
{
    public class MemberProfileDto
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Phone { get; set; } = string.Empty;
        public DateOnly MembershipDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public int MemberId { get; set; }
    }
}
