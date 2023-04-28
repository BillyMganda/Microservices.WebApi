﻿namespace User.Microservice.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ForgotPasswordToken { get; set; } = string.Empty;
        public bool IsTermsAgreed { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}