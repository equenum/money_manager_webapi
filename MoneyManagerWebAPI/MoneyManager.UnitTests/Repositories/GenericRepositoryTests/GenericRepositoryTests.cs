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

namespace MoneyManager.UnitTests.Repositories.GenericRepository
{
    public class GenericRepositoryTests
    {
        #region AddTests

        [Theory]
        [InlineData(1, "TestName")]
        public async Task AddAsync_TestObjectPassed_ProperMethodCalled(int id, string name)
        {
            // Arrange
            var testObject = new TestObject() { Id = id, Name = name };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestObject>>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<TestObject>())).Returns(testObject);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            await repository.AddAsync(testObject);

            // Assert
            context.Verify(x => x.Set<TestObject>());
            dbSetMock.Verify(x => x.Add(It.Is<TestObject>(y => y == testObject)), Times.Once());
            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [InlineData(1, "Name")]
        public async Task AddRangeAsync_ListOfTestObjectPassed_ProperMethodCalled(int id, string name)
        {
            // Arrange
            var firstObject = new TestObject() { Id = id, Name = name };
            var secondObject = new TestObject() { Id = id + 1, Name = name + "2" };
            var testObjects = new List<TestObject>() { firstObject, secondObject };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestObject>>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.AddRange(It.IsAny<List<TestObject>>())).Returns(testObjects);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            await repository.AddRangeAsync(testObjects);

            // Assert
            context.Verify(x => x.Set<TestObject>());
            dbSetMock.Verify(x => x.AddRange(It.Is<List<TestObject>>(y => y == testObjects)), Times.Once());
            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        #endregion

        #region GetTests

        [Theory]
        [InlineData(1, "TestName")]
        public async Task GetAsync_TestObjectPassed_ProperMethodCalled(int id, string name)
        {
            // Arrange
            var testObject = new TestObject() { Id = id, Name = name };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestObject>>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.FindAsync(It.IsAny<int>()).Result).Returns(testObject);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            await repository.GetAsync(testObject.Id);

            // Assert
            context.Verify(x => x.Set<TestObject>());
            dbSetMock.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(1, "TestName")]
        public async Task GetAllAsync_ProperMethodCalled(int id, string name)
        {
            // Arrange
            var firstObject = new TestObject() { Id = id, Name = name };
            var secondObject = new TestObject() { Id = id + 1, Name = name + "2" };
            var testObjects = new List<TestObject>() { firstObject, secondObject }.AsQueryable();

            var dbSetMock = new Mock<DbSet<TestObject>>();
            dbSetMock.As<IDbAsyncEnumerable<TestObject>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TestObject>(testObjects.GetEnumerator()));

            dbSetMock.As<IQueryable<TestObject>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TestObject>(testObjects.Provider));

            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.Expression).Returns(testObjects.Expression);
            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.ElementType).Returns(testObjects.ElementType);
            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.GetEnumerator()).Returns(testObjects.GetEnumerator());

            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            var resultObjects = await repository.GetAllAsync();
            var result = resultObjects.ToList();

            // Assert
            Assert.Equal(testObjects.Count(), resultObjects.Count());
            Assert.Equal(name, result[0].Name);
            Assert.Equal(name + "2", result[1].Name);
        }

        [Theory]
        [InlineData(1, "TestName", 1, 2)]
        public async Task GetAllPagedAsync_ProperMethodCalled(int id, string name, int pageNumber, int pageSize)
        {
            // Arrange
            var firstObject = new TestObject() { Id = id, Name = name };
            var secondObject = new TestObject() { Id = id + 1, Name = name + "2" };
            var testObjects = new List<TestObject>() { firstObject, secondObject }.AsQueryable();

            var dbSetMock = new Mock<DbSet<TestObject>>();
            dbSetMock.As<IDbAsyncEnumerable<TestObject>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TestObject>(testObjects.GetEnumerator()));

            dbSetMock.As<IQueryable<TestObject>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TestObject>(testObjects.Provider));

            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.Expression).Returns(testObjects.Expression);
            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.ElementType).Returns(testObjects.ElementType);
            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.GetEnumerator()).Returns(testObjects.GetEnumerator());

            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            var resultObjects = await repository.GetAllPagedAsync(t => t.Id, pageNumber, pageSize);
            var result = resultObjects.ToList();

            // Assert
            Assert.Equal(testObjects.Count(), resultObjects.Count());
            Assert.Equal(name, result[0].Name);
            Assert.Equal(name + "2", result[1].Name);
        }

        #endregion

        #region FindTests

        [Theory]
        [InlineData(1, "TestName")]
        public async Task FindAsync_TestObjectIdPassed_ProperMethodCalled(int id, string name)
        {
            // Arrange
            var testObject = new TestObject() { Id = id, Name = name };
            var testObjects = new List<TestObject>() { testObject }.AsQueryable();

            var dbSetMock = new Mock<DbSet<TestObject>>();
            dbSetMock.As<IDbAsyncEnumerable<TestObject>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TestObject>(testObjects.GetEnumerator()));

            dbSetMock.As<IQueryable<TestObject>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TestObject>(testObjects.Provider));

            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.Expression).Returns(testObjects.Expression);
            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.ElementType).Returns(testObjects.ElementType);
            dbSetMock.As<IQueryable<TestObject>>().Setup(m => m.GetEnumerator()).Returns(testObjects.GetEnumerator());

            var context = new Mock<DbContext>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            var resultObject = await repository.FindAsync(t => t.Id == testObject.Id);

            // Assert
            Assert.Equal(id, resultObject.Id);
            Assert.Equal(name, resultObject.Name);
        }

        #endregion

        #region RemoveTests

        [Theory]
        [InlineData(1, "TestName")]
        public async Task RemoveAsync_TestObjectPassed_ProperMethodCalled(int id, string name)
        {
            // Arrange
            var testObject = new TestObject() { Id = id, Name = name };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestObject>>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Remove(It.IsAny<TestObject>())).Returns(testObject);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            await repository.RemoveAsync(testObject);

            // Assert
            context.Verify(x => x.Set<TestObject>());
            dbSetMock.Verify(x => x.Remove(It.Is<TestObject>(y => y == testObject)), Times.Once());
            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [InlineData(1, "Name")]
        public async Task RemoveRangeAsync_ListOfTestObjectPassed_ProperMethodCalled(int id, string name)
        {
            // Arrange
            var firstObject = new TestObject() { Id = id, Name = name };
            var secondObject = new TestObject() { Id = id + 1, Name = name + "2" };
            var testObjects = new List<TestObject>() { firstObject, secondObject };

            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<TestObject>>();
            context.Setup(x => x.Set<TestObject>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.RemoveRange(It.IsAny<List<TestObject>>())).Returns(testObjects);

            // Act
            var repository = new GenericRepository<TestObject>(context.Object);
            await repository.RemoveRangeAsync(testObjects);

            // Assert
            context.Verify(x => x.Set<TestObject>());
            dbSetMock.Verify(x => x.RemoveRange(It.Is<List<TestObject>>(y => y == testObjects)), Times.Once());
            context.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        #endregion
    }
}
