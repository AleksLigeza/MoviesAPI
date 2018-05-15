using AutoMapper;
using MoviesAPI.DbModels;
using MoviesAPI.Models;

namespace MoviesAPI.Mapping
{
    public class ActorMappingProfile : Profile
    {
        public ActorMappingProfile()
        {
            CreateMap<ActorRequest, Actor>();
            CreateMap<Actor, ActorResponse>();
        }
    }
}
