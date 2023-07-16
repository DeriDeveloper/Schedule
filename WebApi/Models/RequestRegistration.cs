namespace WebApi.Models
{
    public class RequestRegistration
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int UserRoleId { get; set; }
    }
}
