namespace LibraryManagementSystem.Dtos.User
{
    public class CreateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
    }
}
