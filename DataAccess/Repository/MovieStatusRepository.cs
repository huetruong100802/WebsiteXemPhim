using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MovieStatusRepository : IMovieStatusRepository
    {
        public Task AddMovieStatus(MovieStatus MovieStatus)
        {
            return MovieStatusDao.Instance.Add(MovieStatus);
        }

        public Task Delete(MovieStatus MovieStatus)
        {
            return MovieStatusDao.Instance.Delete(MovieStatus);
        }

        public Task<MovieStatus> GetMovieStatusByIdAsync(string id)
        {
            return MovieStatusDao.Instance.GetMovieStatusByIdAsync(id);
        }

        public IEnumerable<MovieStatus> GetMovieStatus()
        {
            return MovieStatusDao.Instance.GetMovieStatuses();
        }

        public Task Update(MovieStatus MovieStatus)
        {
            return MovieStatusDao.Instance.Update(MovieStatus);
        }

        public Task<MovieStatus> GetMovieStatusByMovieIdAsync(string movieId)
        {
            return MovieStatusDao.Instance.GetMovieStatusByMovieIdAsync(movieId);
        }

        public Task<MovieStatus> GetMovieStatusByStatusIdAsync(int statusId)
        {
            return MovieStatusDao.Instance.GetMovieStatusByStatusIdAsync(statusId);
        }

        public Task DeleteRange(List<MovieStatus> MovieStatus)
        {
            return MovieStatusDao.Instance.DeleteRange(MovieStatus);
        }

        public IEnumerable<MovieStatus> GetMovieStatusesByMovieId(string movieId)
        {
            return MovieStatusDao.Instance.GetMovieStatusesByMovieId(movieId);
        }
    }
}
