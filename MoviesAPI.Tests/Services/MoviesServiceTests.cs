using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoviesAPI.DbModels;
using MoviesAPI.Services;
using MoviesAPI.Tests.Builders;
using Xunit;

namespace MoviesAPI.Tests.Services
{
    public class MoviesServiceTests
    {
        [Fact]
        public async void AddMoviesRange_EmptyList_AddTwoTimes()
        {
            //Arrange
            var list = new List<Movie>()
            {
                new MovieBuilder().WithId(1).WithTitle("title1").WithYear(2000).Build(),
                new MovieBuilder().WithId(2).WithTitle("title2").WithYear(2000).Build(),
            };

            var dbSet = GenerateEnumerableDbSetMock(new List<Movie>().AsQueryable());
            var context = GenerateEnumerableContextMock(dbSet);
            var service = new MoviesService(context.Object);

            //Act
            await service.AddRangeMovies(list);

            //Assert
            dbSet.Verify(x=>x.Add(It.IsAny<Movie>()), Times.Exactly(2));
        }

        [Fact]
        public async void AddMoviesRange_SameMovie_AddOneTime()
        {
            //Arrange
            var list = new List<Movie>()
            {
                new MovieBuilder().WithId(1).WithTitle("title1").WithYear(2000).Build(),
                new MovieBuilder().WithId(2).WithTitle("title2").WithYear(2000).Build(),
            };

            var data = new List<Movie>()
            {
                new MovieBuilder().WithId(1).WithTitle("title1").WithYear(2000).Build(),
            };

            var dbSet = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbSet);
            var service = new MoviesService(context.Object);

            //Act
            await service.AddRangeMovies(list);

            //Assert
            dbSet.Verify(x => x.Add(It.IsAny<Movie>()), Times.Exactly(1));
        }

        [Fact]
        public async void AddMoviesRange_Always_SaveChanges()
        {
            //Arrange
            var list = new List<Movie>()
            {
                new MovieBuilder().WithId(1).WithTitle("title1").WithYear(2000).Build(),
                new MovieBuilder().WithId(2).WithTitle("title2").WithYear(2000).Build(),
            };

            var data = new List<Movie>()
            {
                new MovieBuilder().WithId(1).WithTitle("title1").WithYear(2000).Build(),
            };

            var dbSet = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbSet);
            var service = new MoviesService(context.Object);

            //Act
            await service.AddRangeMovies(list);

            //Assert
            context.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once);
        }

        #region helpers

        private Mock<DbSet<Movie>> GenerateEnumerableDbSetMock(IQueryable<Movie> data)
        {
            var dbSet = new Mock<DbSet<Movie>>();

            dbSet.As<IAsyncEnumerable<Movie>>()
                .Setup(x => x.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Movie>(data.GetEnumerator()));
            dbSet.As<IQueryable<Movie>>()
                .Setup(x => x.Provider)
                .Returns(new TestAsyncQueryProvider<Movie>(data.Provider));
            dbSet.As<IQueryable<Movie>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Movie>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Movie>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            return dbSet;
        }

        private Mock<MoviesContext> GenerateEnumerableContextMock(Mock<DbSet<Movie>> dbSet)
        {
            var contextOptions = new DbContextOptions<MoviesContext>();
            var context = new Mock<MoviesContext>(contextOptions);
            context.Setup(x => x.Movies)
                .Returns(dbSet.Object);
            return context;
        }

        private Mock<MoviesContext> GenerateEnumerableEmptyContextMock()
        {
            var data = new List<Movie>().AsQueryable();
            var dbSet = GenerateEnumerableDbSetMock(data);
            return GenerateEnumerableContextMock(dbSet);
        }

        #endregion
    }
}
