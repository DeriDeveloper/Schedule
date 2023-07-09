using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;
using WebApp.Models;

namespace WebApp.Services
{
    internal class ScheduleAPIService
    {
        private static readonly string urlAPI = "http://localhost:5045/api";


        public static async Task<(HttpStatusCode? status, ResponseAuth? response)> AuthorizationAsync(string login, string password)
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

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var responseAuth = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseAuth>(responseString);

                    return (status: response.StatusCode, response: responseAuth);
                }
                else
                {
                    return (status: response.StatusCode, response: null);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return (status: null, response: null);
            }
        }
    }
}