namespace WebApp.Models
{
    public class RegistrationModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public int UserRoleId { get; set; }
    }
}
