using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MoneyManager.Api;
using MoneyManager.Api.Core.Dtos.Category;
using MoneyManager.Api.Core.Features.Categories.Commands;
using MoneyManager.Api.Core.Features.Categories.Queries;
using MoneyManager.IntegrationTests.Controllers.Common;
using MoneyManager.IntegrationTests.Controllers.Common.v1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace MoneyManager.IntegrationTests.Controllers.v1.CategoriesController
{
    [Collection("CategoriesController")]
    [TestCaseOrderer("MoneyManager.IntegrationTests.Controllers.Common.AlphabeticOrderer", "MoneyManager.IntegrationTests")]
    public class CategoriesControllerTests : IntegrationTest, IClassFixture<CategoriesTestFixtures>
    {
        private readonly CategoriesTestFixtures _fixtures;

        public CategoriesControllerTests(CategoriesTestFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        #region GetAsyncTests

        [Fact]
        public async Task AA_GetAsync_ValidRequest_Returns200()
        {
            // Arrange
            var requestContent = new GetAllCategories.Query()
            {
                PageNumber = 1,
                PageSize = 1
            };

            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent), 
                                           UnicodeEncoding.UTF8, "application/json");

            string uri = $"{ApiRoutes.Categories.GetAsync}?PageNumber=" +
                         $"{requestContent.PageNumber}&PageSize={requestContent.PageSize}";

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Content = stringRequestContent
            };

            // Act
            var response = await TestClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK); 
            response.Content.Should().NotBeNull();
        }

        [Fact]
        public async Task AB_GetAsync_InvalidRequest_Returns404()
        {
            // Arrange
            var requestContent = new GetAllCategories.Query()
            {
                PageNumber = 0,
                PageSize = 0
            };

            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent),
                                           UnicodeEncoding.UTF8, "application/json");

            string uri = $"{ApiRoutes.Categories.GetAsync}?PageNumber=" +
                         $"{requestContent.PageNumber}&PageSize={requestContent.PageSize}";
            
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Content = stringRequestContent
            };

            // Act
            var response = await TestClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Content.Should().NotBeNull();
        }

        #endregion

        #region GetByIdAsyncTests

        [Fact]
        public async Task BA_GetByIdAsync_ValidRequest_Returns200()
        {
            // Act
            var response = await TestClient.GetAsync(
                                            ApiRoutes.Categories.GetByIdAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestCategoryId.ToString()));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Content.Should().NotBeNull();
        }

        [Fact]
        public async Task BB_GetByIdAsync_InvalidRequest_Returns404()
        {
            // Act
            var response = await TestClient.GetAsync(
                                            ApiRoutes.Categories.GetByIdAsync.Replace("{id:int}",
                                            "0"));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Content.Should().NotBeNull();
        }

        #endregion

        #region PatchAsyncTests

        [Fact]
        public async Task CA_PatchAsync_ValidRequest_Returns204()
        {
            // Arrange
            await AuthenticateAsync();

            var request = new UpdateCategory.Command()
            {
                Id = _fixtures.CreatedTestCategoryId,
                Name = "UpdatedTestCategory",
                Description = "Updated test category for integration testing"
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Categories.PatchAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestCategoryId.ToString()), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent); 
        }

        [Fact]
        public async Task CB_PatchAsync_InvalidRequestId_Returns400()
        {
            // Arrange
            await AuthenticateAsync();

            var request = new UpdateCategory.Command()
            {
                Id = _fixtures.CreatedTestCategoryId,
                Name = "UpdatedTestCategory",
                Description = "Updated test category for integration testing"
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Categories.PatchAsync.Replace("{id:int}",
                                            "0"), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CC_PatchAsync_ValidRequestArguments_Returns400()
        {
            // Arrange
            await AuthenticateAsync();

            var request = new UpdateCategory.Command()
            {
                Id = _fixtures.CreatedTestCategoryId,
                Name = null,
                Description = null
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Categories.PatchAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestCategoryId.ToString()), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CD_PatchAsync_WithoutJwtToken_Returns401()
        {
            // Arrange
            var request = new UpdateCategory.Command()
            {
                Id = _fixtures.CreatedTestCategoryId,
                Name = "UpdatedTestCategory",
                Description = "Updated test category for integration testing"
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Categories.PatchAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestCategoryId.ToString()), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        #endregion

        #region PostAsyncTests

        [Fact]
        public async Task E_PostAsync_ValidRequest_Returns201()
        {
            // Arrange
            var request = new CreateCategory.Command()
            {
                Name = "CreateTestCategory",
                Description = "CreateTestCategory",
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(ApiRoutes.Categories.PostAsync, stringRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdCategoty = JsonConvert.DeserializeObject<CategoryDto>(responseContent);

            _fixtures.CategoryToBeRemovedId = createdCategoty.Id;

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Content.Should().NotBeNull();
        }

        #endregion

        #region DeleteAsyncTests

        [Fact]
        public async Task DA_DeleteAsync_ValidRequest_Returns204()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.DeleteAsync(
                                            ApiRoutes.Categories.DeleteAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestCategoryId.ToString()));

            _fixtures.CreatedTestCategoryId = 0;

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task DB_DeleteAsync_InvalidRequestId_Returns400()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.DeleteAsync(
                                            ApiRoutes.Categories.DeleteAsync.Replace("{id:int}",
                                            "0"));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DC_DeleteAsync_WithoutJwtToken_Returns401()
        {
            // Act
            var response = await TestClient.DeleteAsync(
                                            ApiRoutes.Categories.DeleteAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestCategoryId.ToString()));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        #endregion
    }
}
