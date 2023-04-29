using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if(user == null)
                throw new NotFoundException(nameof(user), dto.Email);
            bool IsCorrect = VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt);
            if (IsCorrect == false)
                throw new InvalidCredentialsException("Invalid credentials");
            string Token = CreateJWTToken(dto);
            return Token;
        }

        public async Task<GetUserDto> CreateUserAsync(AddUserDto dto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user != null)
                throw new UserExistsException("user exists, log in instead");

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
            //TODO: SendRegistrationEmail

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

        public async Task<GetUserDto> DeactivateUserAsync(Guid Id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
                throw new NotFoundException(nameof(user), Id);

            user.IsTermsAgreed = false;
            await _dbContext.SaveChangesAsync();

            var GetDto = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = user.CreatedDate,
            };
            return GetDto;
        }

        public string ForgotPaswordToken()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<GetUserDto> GetUserByEmailAsync(string Email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
                throw new NotFoundException(nameof(user), Email);

            var GetDto = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = user.CreatedDate,
            };
            return GetDto;
        }

        public async Task<GetUserDto> GetUserByIdAsync(Guid Id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
                throw new NotFoundException(nameof(user), Id);

            var GetDto = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = user.CreatedDate,
            };
            return GetDto;
        }

        public void SendForgotPasswordEmail(string Email, string Token)
        {
            string EmailFrom = _configuration.GetSection("EmailSettings:From").Value!;
            string Port = _configuration.GetSection("EmailSettings:Port").Value!;
            string EmailPassword = _configuration.GetSection("EmailSettings:Password").Value!;
            string SmtpServer = _configuration.GetSection("EmailSettings:SMTP").Value!;

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(EmailFrom);
            message.To.Add(new MailAddress(Email));
            message.Subject = "Forgot Password";
            message.IsBodyHtml = true;
            message.Body = $"Dear <b>{Email}</b>, <br>a request to reset your password has been made on your account, use this token <b>{Token}</b> to reset your password";
            smtp.Port = Convert.ToInt32(Port);
            smtp.Host = SmtpServer;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(EmailFrom, EmailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public void SendRegistrationEmail(EmailDto dto)
        {
            string EmailFrom = _configuration.GetSection("EmailSettings:From").Value!;
            string Port = _configuration.GetSection("EmailSettings:Port").Value!;
            string EmailPassword = _configuration.GetSection("EmailSettings:Password").Value!;
            string SmtpServer = _configuration.GetSection("EmailSettings:SMTP").Value!;

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(EmailFrom);
            message.To.Add(new MailAddress(dto.Email));
            message.Subject = "User Registration";
            message.IsBodyHtml = true;
            message.Body = $"Dear <b>{dto.Name}</b>, <br> Thank you for your registration to our system";
            smtp.Port = Convert.ToInt32(Port);
            smtp.Host = SmtpServer;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(EmailFrom, EmailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        public bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return ComputedHash.SequenceEqual(PasswordHash);
            }
        }

        public async Task<GetUserDto> UpdateForgotPasswordTokenInDb(string Email, string Token)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if (user == null)
                throw new NotFoundException(nameof(user), Email);

            user.ForgotPasswordToken = Token;
            await _dbContext.SaveChangesAsync();

            var GetDto = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = user.CreatedDate,
            };
            return GetDto;
        }

        public async void ForgotPasswordControllerMethod(string Email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == Email);
            if(user == null)
                throw new NotFoundException(nameof(user), Email);

            string Token = ForgotPaswordToken();
            await UpdateForgotPasswordTokenInDb(Email, Token);
            //TODO: SendForgotPasswordEmail
        }
    }
}
