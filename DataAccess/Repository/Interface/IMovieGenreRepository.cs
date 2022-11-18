using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IMovieGenreRepository
    {
        IEnumerable<MovieGenre> GetMovieGenres();
        Task<MovieGenre> GetMovieGenreByIdAsync(string id);
        Task AddMovieGenre(MovieGenre MovieGenre);
        Task Update(MovieGenre MovieGenre);
        Task Delete(MovieGenre MovieGenre);
        Task DeleteRange(IEnumerable<MovieGenre> MovieGenres);
        IEnumerable<MovieGenre> GetMovieGenresByMovieId(string movieId);
    }
}
