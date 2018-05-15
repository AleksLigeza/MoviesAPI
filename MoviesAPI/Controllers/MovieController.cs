using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System.Collections.Generic;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private IMoviesService _moviesService;
        private IReviewsService _reviewsService;

        public MovieController(IMoviesService moviesService, IReviewsService reviewsService)
        {
            _moviesService = moviesService;
            _reviewsService = reviewsService;
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var list = _moviesService.GetAll();
            
            return Ok(AutoMapper.Mapper.Map<List<MovieResponse>>(list));
        }

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns>Movie if found</returns>
        [HttpGet("{movieId}")]
        public IActionResult Get(int movieId)
        {
            Movie movie = _moviesService.GetById(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<MovieResponse>(movie));
        }

        [HttpGet("{movieId}/reviews")]
        public IActionResult GetReviews(int movieId)
        {
            var reviews = _reviewsService.GetByMovieId(movieId);
            return Ok(AutoMapper.Mapper.Map<List<ReviewResponse>>(reviews));
        }

        /// <summary>
        /// Add new movie to repositorium
        /// </summary>
        /// <param name="movie">new movie</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]MovieRequest movie)
        {
            _moviesService.AddNewMovie(AutoMapper.Mapper.Map<Movie>(movie));

            return Ok();
        }

        /// <summary>
        /// Update movie in repositorium
        /// </summary>
        /// <param name="movie">updated movie</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]MovieRequest movie)
        {
            if (_moviesService.UpdateMovie(AutoMapper.Mapper.Map<Movie>(movie)))
            {
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete movie from repositorium
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns></returns>
        [HttpDelete("{movieId}")]
        public IActionResult Delete(int movieId)
        {
            _moviesService.Remove(movieId);
            return Ok();
        }
    }
}
