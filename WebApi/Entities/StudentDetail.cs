using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class StudentDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
