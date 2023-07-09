using System.Diagnostics;
using System.Text.Json.Serialization;
using WebApp.Models;

namespace WebApp.Services
{
    internal class ScheduleAPIService
    {
        private readonly string urlAPI = "http://localhost:5045/api";


        public async Task<ResponseAuth?> Auth(string login, string password)
        {
            try
            {
                var requestAuth = new RequestAuth()
                {
                    Login = login,
                    Password = password,
                };


                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{urlAPI}/auth");
                request.Headers.Add("Accept", "text/plain");
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestAuth), null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return System.Text.Json.JsonSerializer.Deserialize<ResponseAuth>(responseString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}