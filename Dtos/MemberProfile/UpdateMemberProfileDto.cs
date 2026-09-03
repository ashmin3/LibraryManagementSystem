namespace LibraryManagementSystem.Dtos.MemberProfile
{
    public class UpdateMemberProfileDto
    {
        public string Address { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Phone { get; set; } = string.Empty;
        public DateOnly MembershipDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
