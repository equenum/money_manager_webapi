using MoneyManager.Api.Infrastructure.Data.EntityFramework.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoneyManager.UnitTests.Repositories.UserRepositoryTests
{
    public class UserRepositoryTests
    {
        [Theory]
        [InlineData(1, "Username", "Password", "Role")]
        public async Task RemoveAsync_TestUserPassed_ProperMethodCalled(int id, string username, string password, string role)
        {
            // Arrange
            var testUser = new TestUser() { Id = id, Username = username, Password = password, Role = role };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestUser>>();
            context.Setup(x => x.Set<TestUser>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Remove(It.IsAny<TestUser>())).Returns(testUser);

            // Act
            var repository = new GenericUserRepositoryAsync<TestUser>(context.Object);
            await repository.RemoveAsync(testUser);

            // Assert
            context.Verify(x => x.Set<TestUser>());
            dbSetMock.Verify(x => x.Remove(It.Is<TestUser>(y => y == testUser)), Times.Once());
            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [InlineData("Username", "Password", "Role")]
        public async Task RegisterAsync_TestUserPassed_ProperMethodCalled(string username, string password, string role)
        {
            // Arrange
            var testUser = new TestUser() {Username = username, Password = password, Role = role };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestUser>>();
            context.Setup(x => x.Set<TestUser>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<TestUser>())).Returns(testUser);

            // Act
            var repository = new GenericUserRepositoryAsync<TestUser>(context.Object);
            await repository.RegisterAsync(testUser);

            // Assert
            context.Verify(x => x.Set<TestUser>());
            dbSetMock.Verify(x => x.Add(It.Is<TestUser>(y => y == testUser)), Times.Once());
            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [InlineData(1, "Username", "Password", "Role")]
        public async Task FindAsync_TestObjectIdPassed_ProperMethodCalled(int id, string username, string password, string role)
        {
            // Arrange
            var testUser = new TestUser() { Id = id, Username = username, Password = password, Role = role };
            var testUsers = new List<TestUser>() { testUser }.AsQueryable();

            var dbSetMock = new Mock<DbSet<TestUser>>();
            dbSetMock.As<IDbAsyncEnumerable<TestUser>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TestUser>(testUsers.GetEnumerator()));

            dbSetMock.As<IQueryable<TestUser>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TestUser>(testUsers.Provider));

            dbSetMock.As<IQueryable<TestUser>>().Setup(m => m.Expression).Returns(testUsers.Expression);
            dbSetMock.As<IQueryable<TestUser>>().Setup(m => m.ElementType).Returns(testUsers.ElementType);
            dbSetMock.As<IQueryable<TestUser>>().Setup(m => m.GetEnumerator()).Returns(testUsers.GetEnumerator());

            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestUser>()).Returns(dbSetMock.Object);

            // Act
            var repository = new GenericUserRepositoryAsync<TestUser>(context.Object);
            var resultObject = await repository.FindAsync(t => t.Id == testUser.Id);

            // Assert
            Assert.Equal(id, resultObject.Id);
            Assert.Equal(username, resultObject.Username);
        }
    }
}
