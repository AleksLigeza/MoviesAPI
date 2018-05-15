using System.Collections.Generic;
using System.Linq;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;

namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly MoviesContext _moviesContext;

        public MoviesService(MoviesContext moviesContext)
        {
            _moviesContext = moviesContext;
        }

        public List<Movie> GetAll()
        {
            return _moviesContext.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            Movie foundMovie = _moviesContext.Movies
                  .Where(movie => movie.Id == id)
                  .SingleOrDefault();

            return foundMovie;
        }

        public void AddNewMovie(Movie movie)
        {
            _moviesContext.Movies.Add(movie);
            _moviesContext.SaveChanges();
        }

        public bool UpdateMovie(Movie movie)
        {
            Movie foundMovie = GetById(movie.Id);

            if (foundMovie == null)
            {
                return false;
            }

            foundMovie.Title = movie.Title;
            foundMovie.Year = movie.Year;

            _moviesContext.SaveChanges();

            return true;
        }

        public void Remove(int movieId)
        {
            Movie movie = GetById(movieId);
            _moviesContext.Movies.Remove(movie);
            _moviesContext.SaveChanges();
        }
    }
}
