using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MoviesAPI.DbModels;
using MoviesAPI.Mapping;
using Xunit;

namespace MoviesAPI.Tests.Services
{
    public class TraktTvServiceTests
    {
        //?isolated way to test this service?
        [Fact]
        public async void GetPopularMovies_Always_ListOfMovies()
        {
            //Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MovieMappingProfile>(); 

            });

            var service = new TraktTvService();

            //Act
            var result = await service.GetPopularMovies();

            //Assert
            var list = Assert.IsType<List<Movie>>(result);
        }
    }
}
