using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoviesAPI.DbModels;
using MoviesAPI.Services;
using MoviesAPI.Tests.Builders;
using Xunit;

namespace MoviesAPI.Tests.Services
{
    public class ActorsServiceTests
    {
        [Fact]
        public async Task GetAllActors_ActorCountIs3_ListWith3Actors()
        {
            //Arrange
            var data = new List<Actor>()
            {
                new ActorBuilder().Build(),
                new ActorBuilder().Build(),
                new ActorBuilder().Build(),
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContextMock(dbSet);
            var service = new ActorsService(context.Object);

            //Act
            var resultList = await service.GetAllActors();

            //Assert
            Assert.Equal(3, resultList.Count);
        }

        [Fact]
        public async Task GetActorById_ActorWithIdExists_ActorWithId()
        {
            //Arrange
            var expected = new ActorBuilder().WithId(2).Build();
            var context = GenerateEnumerableEmptyContextMock();
            var service = new ActorsService(context.Object);
            context.Setup(x => x.Actors.FindAsync(2)).ReturnsAsync(expected);

            //Act
            var actor = await service.GetActorById(2);

            //Assert
            Assert.Equal(expected.Id, actor.Id);
        }

        [Fact]
        public async Task AddNewActor_NewActor_AddAndSaveChanges()
        {
            //Arrange
            var actor = new ActorBuilder().WithId(1).Build();

            var dbSet = GenerateEnumerableDbSetMock(new List<Actor>().AsQueryable());
            var context = GenerateEnumerableContextMock(dbSet);
            var service = new ActorsService(context.Object);

            //Act
            await service.AddNewActor(actor);

            //Assert
            dbSet.Verify(x => x.Add(It.IsAny<Actor>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once);
        }

        [Fact]
        public async Task RemoveActor_WithoutActor_False()
        {
            //Arrange
            var context = GenerateEnumerableEmptyContextMock();
            //context.Setup(x => x.Actors.FindAsync(It.IsAny<int>())).ReturnsAsync(null);
            var service = new ActorsService(context.Object);

            //Act
            var result = await service.RemoveActor(1);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveActor_HasActor_True()
        {
            //Arrange
            var actor = new ActorBuilder().WithId(1).Build();
            var data = new List<Actor>()
            {
                actor
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContextMock(dbSet);
            context.Setup(x => x.Actors.FindAsync(It.IsAny<int>())).ReturnsAsync(actor);
            var service = new ActorsService(context.Object);

            //Act
            var result = await service.RemoveActor(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveActor_HasActor_RemoveAndSaveChanges()
        {
            //Arrange
            var actor = new ActorBuilder().WithId(1).Build();
            var data = new List<Actor>()
            {
                actor
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContextMock(dbSet);
            context.Setup(x => x.Actors.FindAsync(It.IsAny<int>())).ReturnsAsync(actor);
            var service = new ActorsService(context.Object);

            //Act
            await service.RemoveActor(1);

            //Assert
            context.Verify(x => x.Actors.Remove(It.IsAny<Actor>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once);
        }

        [Fact]
        public async Task UpdateActor_HasActor_True()
        {
            //Arrange
            var actor = new ActorBuilder().WithId(1).Build();
            var data = new List<Actor>()
            {
                actor
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContextMock(dbSet);
            context.Setup(x => x.Actors.FindAsync(It.IsAny<int>())).ReturnsAsync(actor);
            var service = new ActorsService(context.Object);

            var updatedActor = new ActorBuilder()
                .WithId(1)
                .WithFirstname("John")
                .WithFirstname("Smith")
                .Build();

            //Act
            var result = await service.UpdateActor(updatedActor);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateActor_HasActor_ChangeFirstnameAndLastname()
        {
            //Arrange
            var actor = new ActorBuilder().WithId(1).Build();
            var data = new List<Actor>()
            {
                actor
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContextMock(dbSet);
            context.Setup(x => x.Actors.FindAsync(It.IsAny<int>())).ReturnsAsync(actor);
            var service = new ActorsService(context.Object);

            var updatedActor = new ActorBuilder()
                .WithId(1)
                .WithFirstname("John")
                .WithFirstname("Smith")
                .Build();

            //Act
            var result = await service.UpdateActor(updatedActor);

            //Assert
            Assert.True(result);
            Assert.Equal(updatedActor.Firstname, actor.Firstname);
            Assert.Equal(updatedActor.Lastname, updatedActor.Lastname);
        }

        [Fact]
        public async Task UpdateActor_HasActor_SaveChanges()
        {
            //Arrange
            var actor = new ActorBuilder().WithId(1).Build();
            var data = new List<Actor>()
            {
                actor
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContextMock(dbSet);
            context.Setup(x => x.Actors.FindAsync(It.IsAny<int>())).ReturnsAsync(actor);
            var service = new ActorsService(context.Object);

            var updatedActor = new ActorBuilder()
                .WithId(1)
                .WithFirstname("John")
                .WithFirstname("Smith")
                .Build();

            //Act
            await service.UpdateActor(updatedActor);

            //Assert
            context.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once);
        }

        [Fact]
        public async Task UpdateActor_WithoutActor_False()
        {
            //Arrange
            var actor = new ActorBuilder().WithId(1).Build();
            var data = new List<Actor>()
            {
                actor
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContextMock(dbSet);
            //context.Setup(x => x.Actors.FindAsync(It.IsAny<int>())).ReturnsAsync(null);
            var service = new ActorsService(context.Object);

            var updatedActor = new ActorBuilder()
                .WithId(1)
                .WithFirstname("John")
                .WithFirstname("Smith")
                .Build();

            //Act
            var result = await service.UpdateActor(updatedActor);

            //Assert
            Assert.False(result);
        }

        #region helpers

        private Mock<DbSet<Actor>> GenerateEnumerableDbSetMock(IQueryable<Actor> data)
        {
            var dbSet = new Mock<DbSet<Actor>>();

            dbSet.As<IAsyncEnumerable<Actor>>()
                .Setup(x => x.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Actor>(data.GetEnumerator()));
            dbSet.As<IQueryable<Actor>>()
                .Setup(x => x.Provider)
                .Returns(new TestAsyncQueryProvider<Actor>(data.Provider));
            dbSet.As<IQueryable<Actor>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Actor>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Actor>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            return dbSet;
        }

        private Mock<MoviesContext> GenerateEnumerableContextMock(Mock<DbSet<Actor>> dbSet)
        {
            var contextOptions = new DbContextOptions<MoviesContext>();
            var context = new Mock<MoviesContext>(contextOptions);
            context.Setup(x => x.Actors)
                .Returns(dbSet.Object);
            return context;
        }

        private Mock<MoviesContext> GenerateEnumerableEmptyContextMock()
        {
            var data = new List<Actor>().AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            return GenerateEnumerableContextMock(dbSet);
        }

        #endregion
    }
}
