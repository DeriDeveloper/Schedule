namespace WebApp.Models
{
    public class ProfileInfoResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfileAvatarName { get; set; }
        public int CollegeId { get; set; }
        public UserRole? UserRole { get; set; }
        public StudentDetail? StudentDetail { get; set; }
    }
}
