namespace WebApp.Models
{
    public class ScheduleCell
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public int NumberPair { get; set; }

        public List<User>? Teachers { get; set; }
        public List<Group>? Groups { get; set; }
        public List<Audience>? Audiences { get; set; }

    }
}
