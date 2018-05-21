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

            CreateMap<TraktTvMovie, Movie>()
                .ForMember(dest => dest.Title, source => source.MapFrom(x => x.title))
                .ForMember(dest => dest.Year, source => source.MapFrom(x => x.year));
        }
    }
}
