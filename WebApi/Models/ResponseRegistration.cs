namespace WebApi.Models
{
    public class ResponseRegistration
    {
        public bool IsSuccess { get; set; }
        public List<ErrorMessage>? ErrorMessages { get; set; }
    }
}
