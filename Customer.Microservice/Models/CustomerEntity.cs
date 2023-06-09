﻿namespace Customer.Microservice.Models
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }        
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
