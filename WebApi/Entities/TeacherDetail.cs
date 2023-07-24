using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class TeacherDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? CuratorOfGroupId { get; set; }

    public virtual Group? CuratorOfGroup { get; set; }

    public virtual User User { get; set; } = null!;
}
