using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FollowedMovieRepository : IFollowedMovieRepository
    {
        public Task AddFollowedMovie(FollowedMovie FollowedMovie)
        {
            return FollowedMovieDao.Instance.Add(FollowedMovie);
        }

        public Task Delete(FollowedMovie FollowedMovie)
        {
            return FollowedMovieDao.Instance.Delete(FollowedMovie);
        }

        public Task<FollowedMovie> GetFollowedMovieByIdAsync(string id)
        {
            return FollowedMovieDao.Instance.GetFollowedMovieByIdAsync(id);
        }

        public IEnumerable<FollowedMovie> GetFollowedMovies()
        {
            return FollowedMovieDao.Instance.GetFollowedMovies();
        }

        public Task Update(FollowedMovie FollowedMovie)
        {
            return FollowedMovieDao.Instance.Update(FollowedMovie);
        }
    }
}
