using FluentAssertions;
using MoneyManager.Api.Core;
using MoneyManager.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MoneyManager.UnitTests.TransactionReportTests
{
    public class CalculableTransactionReport_Test
    {
        [Theory]
        [InlineData(2, 500, -500)]
        [InlineData(1, 500, 500)]
        public void CalculateTotal_SingleReport_ReturnsTotal(int type, int amount, int expectedTotal)
        {
            // Arrange
            var transaction = new Transaction() { Type = (TransactionType)type, Amount = amount };

            var report = new TransactionReport();
            report.Transactions.Add(transaction);

            var calculableReport = new CalculableTransactionReport(report);

            // Act
            var actualTotal = calculableReport.CalculateTotal();

            // Assert
            actualTotal.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(1, 100, 1, 500, 600)]
        [InlineData(2, 100, 2, 500, -600)]
        [InlineData(1, 100, 2, 500, -400)]
        public void CalculateTotal_MultipleReports_ReturnsTotal(int firstType, int firstAmount, int secondType, 
                                                                int secondAmount, int expectedTotal)
        {
            // Arrange
            var firstTransaction = new Transaction() { Type = (TransactionType)firstType, Amount = firstAmount };
            var secondTransaction = new Transaction() { Type = (TransactionType)secondType, Amount = secondAmount };

            var report = new TransactionReport();
            report.Transactions.AddRange(new List<Transaction>() { firstTransaction, secondTransaction });

            var calculableReport = new CalculableTransactionReport(report);

            // Act
            var actualTotal = calculableReport.CalculateTotal();

            // Assert
            actualTotal.Should().Be(expectedTotal);
        }

        [Fact]
        public void CalculateTotal_ZeroTransactions_ReturnsZero()
        {
            // Arrange
            int expectedTotal = 0;
            var report = new TransactionReport();
            var calculableReport = new CalculableTransactionReport(report);

            // Act
            var actualTotal = calculableReport.CalculateTotal();

            // Assert
            actualTotal.Should().Be(expectedTotal);
        }
    }
}
