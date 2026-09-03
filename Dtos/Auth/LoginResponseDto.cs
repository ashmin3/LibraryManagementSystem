namespace LibraryManagementSystem.Dtos.Auth
{
    public class LoginResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
