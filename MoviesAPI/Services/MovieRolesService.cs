using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Common;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Services
{
    public class MovieRolesService : IMovieRolesService
    {
        private readonly MoviesContext _moviesContext;

        public MovieRolesService(MoviesContext moviesContext)
        {
            _moviesContext = moviesContext;
        }

        public async Task<List<MovieRole>> GetAllMovieRoles()
        {
            return await _moviesContext.MovieRoles.ToListAsync();
        }

        public async Task<MovieRole> GetMovieRoleById(int id)
        {
            return await _moviesContext.MovieRoles.FindAsync(id);
        }

        public async Task AddNewMovieRole(MovieRole role)
        {
            var movie = await _moviesContext.Movies.FindAsync(role.MovieId);
            if(movie == null)
            {
                throw new MovieApiException("Invalid movie Id");
            }
            var actor = await _moviesContext.Actors.FindAsync(role.ActorId);
            if(actor == null)
            {
                throw new MovieApiException("Invalid actor Id");
            }

            _moviesContext.MovieRoles.Add(role);
            await _moviesContext.SaveChangesAsync();
        }

        public async Task<bool> RemoveMovieRole(int id)
        {
            var role = await GetMovieRoleById(id);
            if(role == null)
            {
                return false;
            }

            _moviesContext.MovieRoles.Remove(role);
            await _moviesContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMovieRole(MovieRole role)
        {
            var foundRole = await GetMovieRoleById(role.Id);
            var movie = await _moviesContext.Movies.FindAsync(role.MovieId);
            var actor = await _moviesContext.Actors.FindAsync(role.ActorId);
            if(foundRole == null || movie == null || actor == null)
            {
                return false;
            }

            foundRole.ActorId = role.ActorId;
            foundRole.MovieId = role.MovieId;

            await _moviesContext.SaveChangesAsync();
            return true;
        }
    }
}
