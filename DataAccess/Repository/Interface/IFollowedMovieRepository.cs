using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IFollowedMovieRepository
    {
        IEnumerable<FollowedMovie> GetFollowedMovies();
        Task<FollowedMovie> GetFollowedMovieByIdAsync(string id);
        Task AddFollowedMovie(FollowedMovie FollowedMovie);
        Task Update(FollowedMovie FollowedMovie);
        Task Delete(FollowedMovie FollowedMovie);
    }
}
