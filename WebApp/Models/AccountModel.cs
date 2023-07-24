namespace WebApp.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole? UserRole { get; set; }
        public College? College { get; set; }
    }
}