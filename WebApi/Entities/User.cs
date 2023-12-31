﻿using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int UserRoleId { get; set; }

    public int? AvatarProfileFileMetadatumId { get; set; }

    public int CollegeId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual FileMetadatum? AvatarProfileFileMetadatum { get; set; }

    public virtual College College { get; set; } = null!;

    public virtual ICollection<HeadOfScheduleDepartmentDetail> HeadOfScheduleDepartmentDetails { get; set; } = new List<HeadOfScheduleDepartmentDetail>();

    public virtual ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();

    public virtual ICollection<TeacherDetail> TeacherDetails { get; set; } = new List<TeacherDetail>();

    public virtual ICollection<UserAccessToken> UserAccessTokens { get; set; } = new List<UserAccessToken>();

    public virtual UserRole UserRole { get; set; } = null!;
}
