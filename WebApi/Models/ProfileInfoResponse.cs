﻿using WebApi.Entities;

namespace WebApi.Models
{
    public class ProfileInfoResponse
    {
        public string Name { get; set; }
        public string ProfileAvatarName { get; set; }
        public UserRole? UserRole { get; set; }
        public StudentDetail? StudentDetail { get; set; }
    }
}
