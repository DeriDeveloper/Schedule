using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class ScheduleCellTeacher
{
    public int ScheduleCellId { get; set; }

    public int TeacherId { get; set; }

    public virtual ScheduleCell ScheduleCell { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;
}
