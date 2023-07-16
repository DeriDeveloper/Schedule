using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class ScheduleTypeWeek
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ScheduleCell> ScheduleCells { get; set; } = new List<ScheduleCell>();
}
