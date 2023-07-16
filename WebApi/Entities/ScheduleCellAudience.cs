using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class ScheduleCellAudience
{
    public int AudienceId { get; set; }

    public int ScheduleCellId { get; set; }

    public virtual Audience Audience { get; set; } = null!;

    public virtual ScheduleCell ScheduleCell { get; set; } = null!;
}
