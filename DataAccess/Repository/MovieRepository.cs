using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public Task AddMovie(Movie movie)
        {
            return MovieDao.Instance.Add(movie);
        }

        public Task Delete(Movie movie)
        {
            return MovieDao.Instance.Delete(movie);
        }

        public Task<Movie> GetMovieByIdAsync(string id)
        {
            return MovieDao.Instance.GetMovieByIdAsync(id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return MovieDao.Instance.GetMovies();
        }

        public IEnumerable<Movie> GetMoviesByGenre(string genreName)
        {
            return MovieDao.Instance.GetMoviesByGenre(genreName);
        }

        public IEnumerable<Movie> GetMoviesByKeyWord(string? keyWord)
        {
            return MovieDao.Instance.GetMoviesByKeyWord(keyWord);
        }

        public IEnumerable<Movie> GetMoviesByUserName(string userName)
        {
            return MovieDao.Instance.GetMoviesByUserName(userName);
        }

        public Task Hide(Movie movie)
        {
            return MovieDao.Instance.Hide(movie);
        }

        public bool IsMovieFollowed(Movie movie, string userName)
        {
            return MovieDao.Instance.IsMovieFollowed(movie, userName);
        }

        public bool MovieIsInGenre(Movie movie, string genreName)
        {
            return MovieDao.Instance.MovieIsInGenre(movie, genreName);
        }

        public bool MovieIsInStatus(Movie movie, string statusName)
        {
            return MovieDao.Instance.MovieIsInStatus(movie, statusName);
        }

        public Task Unhide(Movie movie)
        {
            return MovieDao.Instance.Unhide(movie);
        }

        public Task Update(Movie movie)
        {
            return MovieDao.Instance.Update(movie);
        }

    }
}
