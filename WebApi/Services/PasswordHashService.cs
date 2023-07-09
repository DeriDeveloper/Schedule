namespace WebApi.Services
{
    public class PasswordHashService
    {
        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }
        public static bool VerifyHash(string password, string hashedPassword)
        {
            bool passwordMatched = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            return passwordMatched;
        }
    }
}
