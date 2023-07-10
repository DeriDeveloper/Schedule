using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApp.Services
{
    public class AccountService
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        private CookieHelperService _cookieHelperService;


        public AccountService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;

            _cookieHelperService = new CookieHelperService(JSRuntime);
        }

        public async Task<string?> GetTokenAccessAsync()
        {
            return await _cookieHelperService.GetTokenAccessAsync();
        }
        public async void Logout()
        {
            await _cookieHelperService.DeleteTokenAccessAsync();
        }
    }
}
