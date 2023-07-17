namespace WebApi.Models
{
    public class ScheduleCell
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long TimeStart { get; set; }
        public long TimeEnd { get; set; }
        public int NumberPair { get; set; }
        public int TypeWeekId { get; set; }
        public List<ScheduleCellAudience>? ScheduleCellAudiences { get; set; }
        public List<ScheduleCellGroup>? ScheduleCellGroups { get; set; }
        public List<ScheduleCellTeacher>? ScheduleCellTeachers { get; set; }

    }
}
