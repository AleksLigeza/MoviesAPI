using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Services
{
    public class ActorsService : IActorsService
    {
        private readonly MoviesContext _moviesContext;

        public ActorsService(MoviesContext moviesContext)
        {
            _moviesContext = moviesContext;
        }

        public async Task<List<Actor>> GetAllActors()
        {
            return await _moviesContext.Actors.ToListAsync();
        }

        public async Task<Actor> GetActorById(int id)
        {
            return await _moviesContext.Actors.FindAsync(id);
        }

        public async Task AddNewActor(Actor actor)
        {
            await _moviesContext.Actors.AddAsync(actor);
            await _moviesContext.SaveChangesAsync();
        }

        public async Task<bool> RemoveActor(int id)
        {
            var actor = await GetActorById(id);
            if (actor == null)
            {
                return false;
            }

            _moviesContext.Actors.Remove(actor);
            await _moviesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateActor(Actor actor)
        {
            var foundActor = await GetActorById(actor.Id);
            if(foundActor == null)
            {
                return false;
            }

            foundActor.Firstname = actor.Firstname;
            foundActor.Lastname = actor.Lastname;

            await _moviesContext.SaveChangesAsync();
            return true;
        }
    }
}
