﻿
namespace WebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string HashPassword { get; set; }

        public string Email { get; set; }

        public int UserRoleId { get; set; }

        public UserRole? UserRole { get; set; }
    }
}
