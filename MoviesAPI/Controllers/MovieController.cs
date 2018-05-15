using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IReviewsService _reviewsService;

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

            if(movie == null)
            {
                return NotFound();
            }

            return Ok(AutoMapper.Mapper.Map<MovieResponse>(movie));
        }

        /// <summary>
        /// Get movie reviews
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns>Movie reviews</returns>
        [HttpGet("{movieId}/reviews")]
        public IActionResult GetReviews(int movieId)
        {
            var reviews = _reviewsService.GetByMovieId(movieId);
            return Ok(AutoMapper.Mapper.Map<List<ReviewResponse>>(reviews));
        }

        /// <summary>
        /// Get movie actors
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns>Movie actors/returns>
        [HttpGet("{movieId}/actors")]
        public async Task<IActionResult> GetActors(int movieId)
        {
            var actors = await _moviesService.GetMovieActors(movieId);
            return Ok(AutoMapper.Mapper.Map<List<ActorResponse>>(actors));
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
            if(_moviesService.UpdateMovie(AutoMapper.Mapper.Map<Movie>(movie)))
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

        /// <summary>
        /// Get movie average rate
        /// </summary>
        /// <param name="movieId">movie identifier</param>
        /// <returns>Movie average rate</returns>
        [HttpGet("{movieId}/rate")]
        public async Task<IActionResult> GetAverageRate(int movieId)
        {
            var rate = await _reviewsService.GetAverageRate(movieId);
            return Ok(AutoMapper.Mapper.Map<double>(rate));
        }

        /// <summary>
        /// Get movies by year
        /// </summary>
        /// <param name="year">searched year</param>
        /// <returns>Movies if found</returns>
        [HttpGet("year/{year}")]
        public async Task<IActionResult> Year(int year)
        {
            var movies = await _moviesService.GetMoviesByYear(year);
            return Ok(AutoMapper.Mapper.Map<List<MovieResponse>>(movies));
        }

        /// <summary>
        /// Get movies by title
        /// </summary>
        /// <param name="title">searched title</param>
        /// <returns>Movies if found</returns>
        [HttpGet("title/{title}")]
        public async Task<IActionResult> Title(string title)
        {
            var movies = await _moviesService.GetMoviesByTitle(title);
            return Ok(AutoMapper.Mapper.Map<List<MovieResponse>>(movies));
        }
    }
}
