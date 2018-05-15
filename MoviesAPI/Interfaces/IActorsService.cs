using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.DbModels;

namespace MoviesAPI.Interfaces
{
    public interface IActorsService
    {
        Task<List<Actor>> GetAllActors();
        Task<Actor> GetActorById(int id);
        Task AddNewActor(Actor actor);
        Task<bool> RemoveActor(int id);
        Task<bool> UpdateActor(Actor actor);
    }
}
