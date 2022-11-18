using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IMovieStatusRepository
    {
        IEnumerable<MovieStatus> GetMovieStatus();
        Task<MovieStatus> GetMovieStatusByIdAsync(string id);
        Task<MovieStatus> GetMovieStatusByMovieIdAsync(string movieId);
        Task<MovieStatus> GetMovieStatusByStatusIdAsync(int statusId);
        IEnumerable<MovieStatus> GetMovieStatusesByMovieId(string movieId);
        Task AddMovieStatus(MovieStatus MovieStatus);
        Task Update(MovieStatus MovieStatus);
        Task Delete(MovieStatus MovieStatus);
        Task DeleteRange(List<MovieStatus> MovieStatus);
    }
}
