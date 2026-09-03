using LibraryManagementSystem.Data;
using LibraryManagementSystem.Dtos.Auth;
using LibraryManagementSystem.Interface;
using LibraryManagementSystem.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Services
{
    public class AuthServices :IAuthService
    {
        private readonly LibraryDbContext _libraryDbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LibraryDbContext> _logger;
        public AuthServices(LibraryDbContext libraryDbContext, ILogger<LibraryDbContext> logger, IConfiguration configuration)
        {
            _libraryDbContext = libraryDbContext;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> Auth(LoginDto login)
        {
            var users =await _libraryDbContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == login.Email.ToLower());
            if(users == null)
            {
                throw new KeyNotFoundException("Invalid User");
            }

            var passwordvalid = BCrypt.Net.BCrypt.Verify(
                login.Password,
                users.Password
                );

            if (!passwordvalid)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,users.Name),
                new Claim(ClaimTypes.Email,users.Email),
                new Claim (ClaimTypes.Role,users.Roles)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var signingcredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: Claims,
                signingCredentials: signingcredentials,
                expires: DateTime.UtcNow.AddMinutes(60)
                );


            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenstring = tokenhandler.WriteToken(token);

            return new LoginResponseDto
            {
                Name=users.Name,
                Email=users.Email,
                Roles=users.Roles,
                Token=tokenstring
            };
        }
    }
}
