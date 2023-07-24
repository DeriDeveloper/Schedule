using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class College
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<HeadOfScheduleDepartmentDetail> HeadOfScheduleDepartmentDetails { get; set; } = new List<HeadOfScheduleDepartmentDetail>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
