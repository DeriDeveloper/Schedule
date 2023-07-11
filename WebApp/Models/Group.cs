namespace WebApp.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CollegeId { get; set; }

        public int Year { get; set; }

        public College? College { get; set; }
    }
}
