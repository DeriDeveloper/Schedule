using WebApi.Entities;

namespace WebApi.Models
{
    public class ProfileInfoResponse
    {
        public string Name { get; set; }
        public StudentDetail? StudentDetail { get; set; }
    }
}
