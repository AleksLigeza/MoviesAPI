using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Controllers
{
    public class MovieRoleController : Controller
    {
        private readonly IMovieRolesService _movieRolesService;

        public MovieRoleController(IMovieRolesService movieRolesService)
        {
            _movieRolesService = movieRolesService;
        }

        /// <summary>
        /// Add role to the movie
        /// </summary>
        /// <param name="movieId">existing movie</param>
        /// <param name="actorId">existing actor</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRole(int movieId, int actorId)
        {
            await _movieRolesService.AddNewMovieRole(
                new MovieRole()
                {
                    MovieId = movieId,
                    ActorId = actorId
                });

            return Ok();
        }

        /// <summary>
        /// Delete movie role from repositorium
        /// </summary>
        /// <param name="roleId">role identifier</param>
        /// <returns></returns>
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            await _movieRolesService.RemoveMovieRole(roleId);
            return Ok();
        }
    }
}