using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MoneyManager.Api;
using MoneyManager.Api.Core;
using MoneyManager.Api.Core.Dtos.Transaction;
using MoneyManager.Api.Core.Features.Transactions.Commands;
using MoneyManager.Api.Core.Features.Transactions.Queries;
using MoneyManager.IntegrationTests.Controllers.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoneyManager.IntegrationTests.Controllers.v1.TransactionsController
{
    [Collection("TransactionsController")]
    [TestCaseOrderer("MoneyManager.IntegrationTests.Controllers.Common.AlphabeticOrderer", "MoneyManager.IntegrationTests")]
    public class TransactionsControllerTests : IntegrationTest, IClassFixture<TransactionsTestFixtures>
    {
        private readonly TransactionsTestFixtures _fixtures;

        public TransactionsControllerTests(TransactionsTestFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        #region GetASyncTests

        [Fact]
        public async Task AA_GetAsync_ValidRequest_Returns200()
        {
            var requestContent = new GetAllTransactions.Query()
            {
                PageNumber = 1,
                PageSize = 1
            };

            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent),
                                           UnicodeEncoding.UTF8, "application/json");

            string uri = $"{ApiRoutes.Transactions.GetAsync}?PageNumber=" +
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
            var requestContent = new GetAllTransactions.Query()
            {
                PageNumber = 0,
                PageSize = 0
            };

            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent),
                                           UnicodeEncoding.UTF8, "application/json");

            string uri = $"{ApiRoutes.Transactions.GetAsync}?PageNumber=" +
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
                                            ApiRoutes.Transactions.GetByIdAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestTransactionId.ToString()));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Content.Should().NotBeNull();
        }

        [Fact]
        public async Task BB_GetByIdAsync_InvalidRequest_Returns404()
        {
            // Act
            var response = await TestClient.GetAsync(
                                            ApiRoutes.Transactions.GetByIdAsync.Replace("{id:int}",
                                            "0"));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            response.Content.Should().NotBeNull();
        }

        #endregion

        #region PatchAsync

        [Fact]
        public async Task CA_PatchAsync_ValidRequest_Returns204()
        {
            // Arrange
            await AuthenticateAsync();

            var request = new UpdateTransaction.Command()
            {
                Id = _fixtures.CreatedTestTransactionId,
                Type = TransactionType.Expense,
                CategoryId = _fixtures.CreatedTestCategoryId,
                Description = "Updated test transaction for integration testing",
                Amount = 100
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Transactions.PatchAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestTransactionId.ToString()), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task CB_PatchAsync_InvalidRequestId_Returns400()
        {
            // Arrange
            await AuthenticateAsync();

            var request = new UpdateTransaction.Command()
            {
                Id = _fixtures.CreatedTestTransactionId,
                Type = TransactionType.Expense,
                CategoryId = _fixtures.CreatedTestCategoryId,
                Description = "Updated test transaction for integration testing",
                Amount = 100
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Transactions.PatchAsync.Replace("{id:int}",
                                            "0"), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CC_PatchAsync_InvalidRequestArguments_Returns400()
        {
            // Arrange
            await AuthenticateAsync();

            var request = new UpdateTransaction.Command()
            {
                Id = _fixtures.CreatedTestTransactionId,
                Type = 0,
                CategoryId = 0,
                Description = null,
                Amount = 0
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Transactions.PatchAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestTransactionId.ToString()), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CD_PatchAsync_WithoutJwtToken_Returns401()
        {
            // Arrange
            var request = new UpdateTransaction.Command()
            {
                Id = _fixtures.CreatedTestTransactionId,
                Type = TransactionType.Expense,
                CategoryId = _fixtures.CreatedTestCategoryId,
                Description = "Updated test transaction for integration testing",
                Amount = 100
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PatchAsync(
                                            ApiRoutes.Transactions.PatchAsync.Replace("{id:int}",
                                            _fixtures.CreatedTestTransactionId.ToString()), stringRequest);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        #endregion

        #region PatchAsyncTests

        [Fact]
        public async Task DA_PostAsync_ValidRequest_Returns201()
        {
            // Arrange
            var request = new CreateTransaction.Command()
            {
                Type = TransactionType.Expense,
                CategoryId = _fixtures.CreatedTestCategoryId,
                Description = "CreateTestTransaction",
                Amount = 100
            };

            var stringRequest = new StringContent(
                                    JsonConvert.SerializeObject(request),
                                    UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await TestClient.PostAsync(ApiRoutes.Transactions.PostAsync, stringRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdTransaction = JsonConvert.DeserializeObject<TransactionDto>(responseContent);

            _fixtures.TransactionToBeRemovedId = createdTransaction.Id; 

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Content.Should().NotBeNull();
        }

        #endregion
    }
}
