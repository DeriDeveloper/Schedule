using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class HeadOfScheduleDepartmentDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CollegeId { get; set; }

    public virtual College College { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
