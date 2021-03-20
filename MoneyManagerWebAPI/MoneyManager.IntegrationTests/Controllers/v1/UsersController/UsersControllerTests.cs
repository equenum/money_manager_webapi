using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MoneyManager.Api;
using MoneyManager.Api.Core.Dtos.User;
using MoneyManager.Api.Core.Features.Users.Commands;
using MoneyManager.Api.Core.Features.Users.Queries;
using MoneyManager.IntegrationTests.Controllers.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoneyManager.IntegrationTests.Controllers.v1.UsersController
{
    [Collection("UsersController")]
    [TestCaseOrderer("MoneyManager.IntegrationTests.Controllers.Common.AlphabeticOrderer", "MoneyManager.IntegrationTests")]
    public class UsersControllerTests : IntegrationTest, IClassFixture<UsersTestFixtures>
    {
        private readonly UsersTestFixtures _fixtures;

        public UsersControllerTests(UsersTestFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        #region AuthenticateAsyncTests

        [Fact]
        public async Task AA_AuthenticateAsync_ValidRequest_Returns200()
        {
            // Arrange
            var request = new AuthenticateUser.Query()
            {
                Username = _fixtures.TestUserUsername,
                Password = _fixtures.TestUserPassword
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(
                                            ApiRoutes.Users.AuthenticateAsync, 
                                            stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task AB_AuthenticateAsync_InvalidRequest_Returns404()
        {
            // Arrange
            var request = new AuthenticateUser.Query()
            {
                Username = "InvalidUser",
                Password = "InvalidPass"
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(
                                            ApiRoutes.Users.AuthenticateAsync,
                                            stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        #endregion

        #region RegisterAsyncTests

        [Fact]
        public async Task BA_RegisterAsync_ValidRequest_Returns201()
        {
            // Arrange
            var request = new RegisterUser.Command()
            {
                Username = "RegTestUser",
                Password = "RegTestUser",
                Role = "ADMIN"
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(ApiRoutes.Users.RegisterAsync, stringRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<UserDto>(responseContent);

            _fixtures.TestUserToBeRemovedId = createdUser.Id;

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Content.Should().NotBeNull();
        }

        [Fact]
        public async Task BB_RegisterAsync_InvalidRequestExistingUser_Returns409()
        {
            // Arrange
            var request = new RegisterUser.Command()
            {
                Username = _fixtures.TestUserUsername,
                Password = _fixtures.TestUserPassword,
                Role = _fixtures.TestUserRole
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(
                                            ApiRoutes.Users.RegisterAsync, 
                                            stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status409Conflict);
            response.Content.Should().NotBeNull();
        }

        #endregion

        #region DeleteAsyncTests

        [Fact]
        public async Task CA_DeleteAsyncTests_ValidRequest_Returns204()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.DeleteAsync(
                                            ApiRoutes.Users.DeleteAsync.Replace("{id:int}",
                                            _fixtures.TestUserToBeRemovedId.ToString()));

            _fixtures.TestUserToBeRemovedId = 0;

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task CB_DeleteAsyncTests_InvalidRequestId_Returns400()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.DeleteAsync(
                                            ApiRoutes.Users.DeleteAsync.Replace("{id:int}",
                                            "0"));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CC_DeleteAsyncTests_WithoutJwtToken_Returns401()
        {
            // Act
            var response = await TestClient.DeleteAsync(
                                            ApiRoutes.Users.DeleteAsync.Replace("{id:int}",
                                            "0"));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        #endregion
    }
}
