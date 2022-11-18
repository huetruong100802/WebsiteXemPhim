using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IMoviePeopleRepository
    {
        IEnumerable<MoviePeople> GetMoviePeople();
        Task<MoviePeople> GetMoviePeopleByIdAsync(string id);
        Task<MoviePeople> GetMoviePeopleByMovieIdAsync(string movieId);
        Task<MoviePeople> GetMoviePeopleByRoleIdAsync(string roleId);
        IEnumerable<MoviePeople> GetMoviePeoplesByMovieId(string movieId);
        Task AddMoviePeople(MoviePeople MoviePeople);
        Task Update(MoviePeople MoviePeople);
        Task Delete(MoviePeople MoviePeople);
        Task DeleteRange(List<MoviePeople> moviePeople);
    }
}
