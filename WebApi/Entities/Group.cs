using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CollegeId { get; set; }

    public int Year { get; set; }

    public virtual College College { get; set; } = null!;

    public virtual ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();
}
