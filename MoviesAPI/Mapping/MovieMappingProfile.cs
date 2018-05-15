using AutoMapper;
using MoviesAPI.DbModels;
using MoviesAPI.Models;

namespace MoviesAPI.Mapping
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<MovieRequest, Movie>();
            CreateMap<Movie, MovieResponse>();
        }
    }
}
