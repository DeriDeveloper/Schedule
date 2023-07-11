﻿using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WebApp.Models;

namespace WebApp.Services
{
    internal class ScheduleAPIService
    {
        private static readonly string urlAPI = "http://localhost:5045/api";


        public static async Task<List<College>> GetCollegesAsync()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{urlAPI}/colleges/Get");
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();


                    var colleges = JsonConvert.DeserializeObject<List<College>>(responseContent);

                    if (colleges is not null)
                        return colleges;
                    else return new List<College>();
                }
                else
                {
                    return new List<College>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return new List<College>();
            }
        }
        public static async Task<List<ResponseGroup>?> GetGroups(int collegeId, int year)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{urlAPI}/groups/Get?collegeId={collegeId}&year={year}");
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<ResponseGroup>>(responseContent);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
        public static async Task<bool> ValidTokenAsync(string token)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{urlAPI}/TokenValidation?token={token}");
                var response = await client.SendAsync(request);

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    return true;

                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    // Записывать все данные о результате мб сломалось что то на стороне сервера
                    return false;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

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
    
        

        public class Account
        {
            public static async Task<ProfileInfoResponse?> GetProfileInfoAsync(string token)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{urlAPI}/account/GetProfileInfo?accessToken={token}");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ProfileInfoResponse>(responseContent);
                }
                else
                {
                    return null;
                }
            }
            public static async Task<bool?> SaveProfileInfoAsync(string token, string name, int collegeId, int groupId)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{urlAPI}/account/SaveProfileInfo?accessToken={token}&name={name}&collegeId={collegeId}&groupId={groupId}");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}