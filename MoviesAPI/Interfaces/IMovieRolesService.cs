using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.DbModels;

namespace MoviesAPI.Interfaces
{
    public interface IMovieRolesService
    {
        Task<List<MovieRole>> GetAllMovieRoles();
        Task<MovieRole> GetMovieRoleById(int id);
        Task AddNewMovieRole(MovieRole role);
        Task<bool> RemoveMovieRole(int id);
        Task<bool> UpdateMovieRole(MovieRole role);
    }
}
