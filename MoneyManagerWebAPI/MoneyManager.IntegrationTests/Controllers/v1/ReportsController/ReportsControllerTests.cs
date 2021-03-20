using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MoneyManager.Api;
using MoneyManager.Api.Core.Features.TransactionReports.Queries;
using MoneyManager.IntegrationTests.Controllers.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoneyManager.IntegrationTests.Controllers.v1.ReportsController
{
    [Collection("ReportsController")]
    [TestCaseOrderer("MoneyManager.IntegrationTests.Controllers.Common.AlphabeticOrderer", "MoneyManager.IntegrationTests")]
    public class ReportsControllerTests : IntegrationTest, IClassFixture<ReportsTestFixtures>
    {
        private readonly ReportsTestFixtures _fixtures;

        public ReportsControllerTests(ReportsTestFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        #region GetTotalByDateAsyncTests

        [Fact]
        public async Task AA_GetTotalByDateAsync_ValidRequest_Returns200()
        {
            // Arrange
            var requestContent = new GetTotalByDate.Query()
            {
                ReportDate = _fixtures.TestCategoryDate
            };
         
            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent),
                                           UnicodeEncoding.UTF8, "application/json");

            var date = requestContent.ReportDate;

            string uri = $"{ApiRoutes.Reports.GetTotalByDateAsync}?ReportDate=" +
                         $"{date.Year}-{date.Month}-{date.Day}";

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
        public async Task AB_GetTotalByDateAsync_InvalidRequest_Returns404()
        {
            // Arrange
            var requestContent = new GetTotalByDate.Query()
            {
                ReportDate = new DateTime(2000, 1, 1)
            };

            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent),
                                           UnicodeEncoding.UTF8, "application/json");

            var date = requestContent.ReportDate;

            string uri = $"{ApiRoutes.Reports.GetTotalByDateAsync}?ReportDate=" +
                         $"{date.Year}-{date.Month}-{date.Day}";

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

        #region GetTotalByPeriodAsync

        [Fact]
        public async Task BA_GetTotalByPeriodAsync_ValidRequest_Returns200()
        {
            // Arrange
            var requestContent = new GetTotalByPeriod.Query()
            {
                BeginningDate = _fixtures.TestCategoryDate,
                EndDate = _fixtures.TestCategoryDate
            };

            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent),
                                           UnicodeEncoding.UTF8, "application/json");

            var date = requestContent.BeginningDate;

            string uri = $"{ApiRoutes.Reports.GetTotalByPeriodAsync}?BeginningDate=" +
                         $"{date.Year}-{date.Month}-{date.Day}&EndDate=" +
                         $"{date.Year}-{date.Month}-{date.Day}";

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
        public async Task BB_GetTotalByPeriodAsync_InvalidRequest_Returns404()
        {
            // Arrange
            var requestContent = new GetTotalByPeriod.Query()
            {
                BeginningDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 1)
            };

            var stringRequestContent = new StringContent(
                                           JsonConvert.SerializeObject(requestContent),
                                           UnicodeEncoding.UTF8, "application/json");

            var date = requestContent.BeginningDate;

            string uri = $"{ApiRoutes.Reports.GetTotalByPeriodAsync}?BeginningDate=" +
                         $"{date.Year}-{date.Month}-{date.Day}&EndDate=" +
                         $"{date.Year}-{date.Month}-{date.Day}";

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
    }
}
