using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        public Task AddMovieGenre(MovieGenre MovieGenre)
        {
            return MovieGenreDao.Instance.Add(MovieGenre);
        }

        public Task Delete(MovieGenre MovieGenre)
        {
            return MovieGenreDao.Instance.Delete(MovieGenre);
        }

        public Task DeleteRange(IEnumerable<MovieGenre> MovieGenres)
        {
            return MovieGenreDao.Instance.DeleteRange(MovieGenres.ToList());
        }

        public Task<MovieGenre> GetMovieGenreByIdAsync(string id)
        {
            return MovieGenreDao.Instance.GetMovieGenreByIdAsync(id);
        }

        public IEnumerable<MovieGenre> GetMovieGenres()
        {
            return MovieGenreDao.Instance.GetMovieGenres();
        }

        public IEnumerable<MovieGenre> GetMovieGenresByMovieId(string movieId)
        {
            return MovieGenreDao.Instance.GetMovieGenresByMovieId(movieId);
        }

        public Task Update(MovieGenre MovieGenre)
        {
            return MovieGenreDao.Instance.Update(MovieGenre);
        }
    }
}
