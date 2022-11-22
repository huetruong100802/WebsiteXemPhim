using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        Task<Movie> GetMovieByIdAsync(string id);
        Task AddMovie(Movie movie);
        Task Update(Movie movie);
        Task Delete(Movie movie);
        Task Hide(Movie movie);
        Task Unhide(Movie movie);
        bool MovieIsInGenre(Movie movie, string genrename);
        IEnumerable<Movie> GetMoviesByGenre(string genreName);
        IEnumerable<Movie> GetMoviesByKeyWord(string? keyWord);
        bool MovieIsInStatus(Movie movie, string statusName);
        bool IsMovieFollowed(Movie movie, string userName);
        public IEnumerable<Movie> GetMoviesByUserName(string userName);
    }
}
