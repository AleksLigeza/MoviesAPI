using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesAPI.DbModels;

namespace MoviesAPI.Interfaces
{
    public interface IMoviesService
    {
        List<Movie> GetAll();

        Movie GetById(int id);

        void AddNewMovie(Movie movie);

        bool UpdateMovie(Movie movie);

        void Remove(int movieId);
        Task<List<Actor>> GetMovieActors(int id);
        Task<List<Movie>> GetMoviesByYear(int year);
        Task<List<Movie>> GetMoviesByTitle(string title);

        Task<int> AddRangeMovies(List<Movie> list);
    }
}
