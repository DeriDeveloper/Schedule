using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class UserAccessToken
{
    public Guid Guid { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public virtual User User { get; set; } = null!;
}
