using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace WebApp.Services
{

    public class CookieHelperService
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        public CookieHelperService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        public async Task<bool> DeleteTokenAccessAsync()
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("eval", $"document.cookie = 'TokenAccess=; expires={DateTime.UtcNow.ToString("R")}; path=/';");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public async Task<bool> UpdateTokenAccessAsync(string token)
        {
            try
            {
                await JSRuntime.InvokeVoidAsync("eval", $"document.cookie = 'TokenAccess={token}; expires={DateTime.UtcNow.AddMonths(6).ToString("R")}; path=/';");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

        }

        public async Task<string?> GetTokenAccessAsync()
        {
            return await GetCookieAsync("TokenAccess");
        }

        private async Task<string?> GetCookieAsync(string key)
        {
            try
            {
                var result = await JSRuntime.InvokeAsync<string>("eval", "document.cookie");
                var cookies = result.Split(";");

                foreach (var cookie in cookies)
                {
                    if (string.IsNullOrEmpty(cookie))
                    {
                        continue;
                    }


                    var parts = cookie.Split('=');
                    var keyCookie = parts[0].Trim();
                    var valueCookie = parts[1].Trim();

                    if (keyCookie == key)
                    {
                        return valueCookie;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
