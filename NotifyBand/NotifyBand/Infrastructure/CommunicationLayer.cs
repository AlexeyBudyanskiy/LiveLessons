using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotifyBand.Models;
using Newtonsoft.Json.Linq;

namespace NotifyBand.Infrastructure
{
    public class CommunicationLayer
    {
        private const string ConnectionStringName = "Host";
        private static readonly string Host; 

        static CommunicationLayer()
        {
            Host = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }
        public async Task<string> GetToken()
        {
            var credentials = GetCredentials();
            var requestMessage = BuildTokenRequestMessage(credentials);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var stringJson = await response.Content.ReadAsStringAsync();
                var token = JToken.Parse(stringJson);
                var accessToken = token["access_token"];

                return accessToken.ToString();
            }

            if(response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                Console.Clear();
                Console.WriteLine("Invalid credentials. Try again.");
                return await GetToken();
            }

            Console.WriteLine("Server has some problem. Please try again later.");
            Console.ReadLine();
            throw new ServerException("Server exception");
        }

        public async Task<User> GetUserInfo(string accessToken)
        {
            var requestMessage = BuildUserRequestMessage(accessToken);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);
            var stringJson = await response.Content.ReadAsStringAsync();
            var jsonSerializer = new JsonSerializer();
            var user = jsonSerializer.Deserialize<User>(new JsonTextReader(new StringReader(stringJson)));

            return user;
        }

        public async Task<List<Course>> GetUserCoursesInfo(string accessToken)
        {
            var requestMessage = BuildCoursesRequestMessage(accessToken);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);
            var stringJson = await response.Content.ReadAsStringAsync();
            var jsonSerializer = new JsonSerializer();
            var courses = jsonSerializer.Deserialize<List<Course>>(new JsonTextReader(new StringReader(stringJson)));

            return courses;
        }

        public async Task<List<Appointment>> GetUserAppointmentsInfo(string accessToken)
        {
            var requestMessage = BuildAppointmentsRequestMessage(accessToken);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);
            var stringJson = await response.Content.ReadAsStringAsync();
            var jsonSerializer = new JsonSerializer();
            var appointments = jsonSerializer.Deserialize<List<Appointment>>(new JsonTextReader(new StringReader(stringJson)));

            return appointments;
        }

        private HttpRequestMessage BuildTokenRequestMessage(Credentials credentials)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{Host}/token");
            var stringContent = $"grant_type=password&username={credentials.UserName}&password={credentials.Password}";
            requestMessage.Content = new StringContent(stringContent);
            requestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
            requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            return requestMessage;
        }

        private HttpRequestMessage BuildUserRequestMessage(string accessToken)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Host}/api/users/me");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            return requestMessage;
        }

        private HttpRequestMessage BuildCoursesRequestMessage(string accessToken)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Host}/api/courses/my");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            return requestMessage;
        }

        private HttpRequestMessage BuildAppointmentsRequestMessage(string accessToken)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{Host}/api/appointments/my");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
            requestMessage.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            return requestMessage;
        }

        private Credentials GetCredentials()
        {
            Console.WriteLine("Insert your username and password");
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var pass = Console.ReadLine();
            Console.Clear();

            var result = new Credentials
            {
                Password = pass,
                UserName = username
            };

            return result;
        }
    }
}
