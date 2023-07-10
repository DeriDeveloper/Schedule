using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class College
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
