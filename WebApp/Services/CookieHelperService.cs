namespace WebApp.Services
{
    public class CookieHelperService
    {
        public static void AddCookie(IResponseCookies cookies, string key, string value, CookieOptions option)
        {
            

            cookies.Append(key, value, option);
        }
    }
}
