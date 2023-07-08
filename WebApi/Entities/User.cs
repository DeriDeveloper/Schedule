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

    public virtual UserRole UserRole { get; set; } = null!;
}
