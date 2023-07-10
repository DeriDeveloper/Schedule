using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class UserAccessToken
{
    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
