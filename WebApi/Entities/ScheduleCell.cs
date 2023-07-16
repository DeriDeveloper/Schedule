using System;
using System.Collections.Generic;

namespace WebApi.Entities;

public partial class ScheduleCell
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public long TimeStart { get; set; }

    public long TimeEnd { get; set; }

    public int NumberPair { get; set; }

    public int TypeWeekId { get; set; }

    public virtual ScheduleTypeWeek TypeWeek { get; set; } = null!;
}
