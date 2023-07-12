namespace WebApi.Services
{
    public class HashService
    {
        public static string Hash(string text)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashed = BCrypt.Net.BCrypt.HashPassword(text, salt);

            return hashed;
        }
        public static bool VerifyHash(string text, string hashed)
        {
            bool textMatched = BCrypt.Net.BCrypt.Verify(text, hashed);

            return textMatched;
        }
    }
}
