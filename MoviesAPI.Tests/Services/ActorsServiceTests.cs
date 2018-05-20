using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var context = GenerateEnumerableContext(dbSet);
            var actorsService = new ActorsService(context.Object);

            //Act
            var resultList = await actorsService.GetAllActors();

            //Assert
            Assert.Equal(3, resultList.Count);
        }

        //todo: fix findAsyncTest
        [Fact]
        public async Task GetActorById_ActorWithIdExists_ReturnsActorWithId()
        {
            //Arrange
            var data = new List<Actor>()
            {
                new ActorBuilder().WithId(1).Build(),
                new ActorBuilder().WithId(2).Build(),
                new ActorBuilder().WithId(3).Build(),
            }.AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            var context = GenerateEnumerableContext(dbSet);
            var actorsService = new ActorsService(context.Object);

            //Act
            var actor = await actorsService.GetActorById(2);

            //Assert
            Assert.Equal(2, actor.Id);
        }


        #region helpers

        private Mock<DbSet<Actor>> GenerateEnumerableDbSetMock(IQueryable<Actor> data)
        {
            var dbSet = new Mock<DbSet<Actor>>();

            dbSet.As<IAsyncEnumerable<Actor>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Actor>(data.GetEnumerator()));
            dbSet.As<IQueryable<Actor>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Actor>(data.Provider));
            dbSet.As<IQueryable<Actor>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Actor>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Actor>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            return dbSet;
        }

        private Mock<MoviesContext> GenerateEnumerableContext(Mock<DbSet<Actor>> dbSet)
        {
            var contextOptions = new DbContextOptions<MoviesContext>();
            var context = new Mock<MoviesContext>(contextOptions);
            context.Setup(x => x.Actors)
                .Returns(dbSet.Object);
            return context;
        }

        #endregion
    }
}
