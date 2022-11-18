using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class GenreRepository : IGenreRepository
    {
        public Task AddGenre(Genre Genre)
        {
            return GenreDao.Instance.Add(Genre);
        }

        public Task Delete(Genre Genre)
        {
            return GenreDao.Instance.Delete(Genre);
        }

        public Task<Genre> GetGenreByIdAsync(string id)
        {
            return GenreDao.Instance.GetGenreByIdAsync(id);
        }

        public Task<Genre> GetGenreByNameAsync(string name)
        {
            return GenreDao.Instance.GetGenreByNameAsync(name);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return GenreDao.Instance.GetGenres();
        }

        public IEnumerable<Genre> GetGenresByMovieId(string movieId)
        {
            return GenreDao.Instance.GetGenresByMovieId(movieId);
        }

        public Task Update(Genre Genre)
        {
            return GenreDao.Instance.Update(Genre);
        }
    }
}
