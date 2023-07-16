using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class ScheduleCellGroup
{
    public int ScheduleCellId { get; set; }

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ScheduleCell ScheduleCell { get; set; } = null!;
}
