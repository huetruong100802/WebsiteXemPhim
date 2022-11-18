using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
        Task<Genre> GetGenreByIdAsync(string id);
        Task<Genre> GetGenreByNameAsync(string name);
        Task AddGenre(Genre Genre);
        Task Update(Genre Genre);
        Task Delete(Genre Genre);
        IEnumerable<Genre> GetGenresByMovieId(string movieId);
    }
}
