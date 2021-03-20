using Microsoft.AspNetCore.Mvc.Testing;
using MoneyManager.Api;
using MoneyManager.Api.Core.Dtos.User;
using MoneyManager.Api.Core.Features.Users.Commands;
using MoneyManager.Api.Core.Features.Users.Queries;
using MoneyManager.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.IntegrationTests.Controllers.Common
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            await RegisterTestUserAsync();

            var authenticatedUser = await AuthenticateTestUserAsync();

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticatedUser.Token);

            await DeleteTestUserAsync(authenticatedUser.Id);
        }

        private async Task RegisterTestUserAsync()
        {
            var request = new RegisterUser.Command()
            {
                Username = "IntegrTestAdmin",
                Password = "IntegrPassword",
                Role = "ADMIN"
            };

            var serialisedRequest = JsonConvert.SerializeObject(request);

            var stringRequest = new StringContent(serialisedRequest, UnicodeEncoding.UTF8, "application/json");

            var response = await TestClient.PostAsync(ApiRoutes.Users.RegisterAsync, stringRequest);
            response.EnsureSuccessStatusCode();
        }

        private async Task<UserAuthDto> AuthenticateTestUserAsync()
        {
            var request = new AuthenticateUser.Query()
            {
                Username = "IntegrTestAdmin",
                Password = "IntegrPassword"
            };

            var serializedRequest = JsonConvert.SerializeObject(request);
            var stringRequest = new StringContent(serializedRequest, UnicodeEncoding.UTF8, "application/json");

            var response = await TestClient.PostAsync(ApiRoutes.Users.AuthenticateAsync, stringRequest);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var authenticatedUser = JsonConvert.DeserializeObject<UserAuthDto>(responseContent);

            return authenticatedUser;
        }

        private async Task DeleteTestUserAsync(int id)
        {
            var response = await TestClient.DeleteAsync(ApiRoutes.Users.DeleteAsync.Replace("{id:int}", 
                                                        id.ToString()));

            response.EnsureSuccessStatusCode();
        }
    }
}
