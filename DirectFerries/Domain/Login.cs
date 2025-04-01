using DirectFerries.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DirectFerries.Domain
{
    internal class Login
    {
        private readonly string _baseUrl;

        public Login(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<HttpClient?> AuthenticateAsync()
        {
            var client = new HttpClient();

            Console.WriteLine("Enter username:");
            var username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            var password = Console.ReadLine();

            var loginPayload = new
            {
                username,
                password,
                expiresInMins = 30
            };

            var loginContent = new StringContent(JsonConvert.SerializeObject(loginPayload));
            loginContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Log("Sending login request...");
            var loginResponse = await client.PostAsync($"{_baseUrl}/auth/login", loginContent);
            var loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

            Log("Login Status Code: " + loginResponse.StatusCode);
            Log("Login Response Body: " + loginResponseBody);

            if (!loginResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Login failed.");
                return null;
            }

            var loginResult = JsonConvert.DeserializeObject<AuthResponse>(loginResponseBody);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);

            return client;
        }

        private void Log(string message)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            Console.WriteLine(logMessage);
            File.AppendAllText("log.txt", logMessage + Environment.NewLine);
        }
    }
}


