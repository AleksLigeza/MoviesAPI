using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.DbModels;

namespace MoviesAPI.Interfaces
{
    public interface ITraktTVService
    {
        Task<List<Movie>> GetPopularMovies();
    }
}
