using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Simple_Online_Survey_Application.Context;
using Simple_Online_Survey_Application.Entity.Dtos;
using Simple_Online_Survey_Application.Entity.Entities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Simple_Online_Survey_Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(UserRegisterDto dto)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                    return "Email already exists.";

                using var hmac = new HMACSHA512();
                var salt = Convert.ToBase64String(hmac.Key);
                var hash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password)));

                var user = new User
                {
                    Email = dto.Email,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return "User registered successfully.";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
                if (user == null)
                    return null;

                var saltBytes = Convert.FromBase64String(user.PasswordSalt);
                using var hmac = new HMACSHA512(saltBytes);
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dto.Password)));

                if (computedHash != user.PasswordHash)
                    return null;

                return GenerateJwtToken(user.Id, "User");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during login: {ex.Message}");
            }
        }


        private string GenerateJwtToken(int userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}