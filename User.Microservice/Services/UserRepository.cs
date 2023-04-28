using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User.Microservice.Data;
using User.Microservice.DTOs;
using User.Microservice.Exceptions;
using User.Microservice.Models;

namespace User.Microservice.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserRepository(UserDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<GetUserDto> ChangeUserPasswordAsync(ChangePasswordDto entity, byte[] Hash, byte[] Salt)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ForgotPasswordToken == entity.Token);
            if (user == null)
                throw new NotFoundException(nameof(user), entity.Token);

            user.PasswordHash = Hash;
            user.PasswordSalt = Salt;
            user.ForgotPasswordToken = "";
            await _dbContext.SaveChangesAsync();

            var GetDto = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = DateTime.Now,
            };
            return GetDto;
        }

        public string CreateJWTToken(LoginDto dto)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, dto.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );
            string JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return JwtToken;
        }

        public void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateRefreshToken()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<GetUserDto> CreateUserAsync(AddUserDto dto)
        {
            CreatePasswordHash(dto.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            var NewUser = new UserModel
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ForgotPasswordToken = "",
                IsTermsAgreed = true,
                RefreshToken = CreateRefreshToken(),
            };
            _dbContext.Users.Add(NewUser);
            await _dbContext.SaveChangesAsync();

            var GetDto = new GetUserDto
            {
                Id = NewUser.Id,
                FirstName = NewUser.FirstName,
                LastName = NewUser.LastName,
                Email = NewUser.Email,
                CreatedDate = NewUser.CreatedDate,
            };
            return GetDto;
        }

        public Task<GetUserDto> DeactivateUserAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public string ForgotPaswordToken()
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> GetUserByEmailAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> GetUserByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void SendForgotPasswordEmail(EmailDto dto)
        {
            throw new NotImplementedException();
        }

        public void SendRegistrationEmail(EmailDto dto)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            throw new NotImplementedException();
        }
    }
}
