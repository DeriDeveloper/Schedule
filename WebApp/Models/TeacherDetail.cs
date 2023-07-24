namespace WebApp.Models
{
    public class TeacherDetail
    {
        public int Id { get; set; }

        public int? CuratorOfGroupId { get; set; }
        public Group? CuratorOfGroup { get; set; }
    }
}
