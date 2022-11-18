using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MoviePeopleRepository : IMoviePeopleRepository
    {
        public Task AddMoviePeople(MoviePeople MoviePeople)
        {
            return MoviePeopleDao.Instance.Add(MoviePeople);
        }

        public Task Delete(MoviePeople MoviePeople)
        {
            return MoviePeopleDao.Instance.Delete(MoviePeople);
        }

        public Task<MoviePeople> GetMoviePeopleByIdAsync(string id)
        {
            return MoviePeopleDao.Instance.GetMoviePeopleByIdAsync(id);
        }

        public IEnumerable<MoviePeople> GetMoviePeople()
        {
            return MoviePeopleDao.Instance.GetMoviePeoples();
        }

        public Task Update(MoviePeople MoviePeople)
        {
            return MoviePeopleDao.Instance.Update(MoviePeople);
        }

        public Task<MoviePeople> GetMoviePeopleByMovieIdAsync(string movieId)
        {
            return MoviePeopleDao.Instance.GetMoviePeopleByMovieIdAsync(movieId);
        }

        public Task<MoviePeople> GetMoviePeopleByRoleIdAsync(string roleId)
        {
            return MoviePeopleDao.Instance.GetMoviePeopleByRoleIdAsync(roleId);
        }

        public Task DeleteRange(List<MoviePeople> moviePeople)
        {
            return MoviePeopleDao.Instance.DeleteRange(moviePeople);
        }

        public IEnumerable<MoviePeople> GetMoviePeoplesByMovieId(string movieId)
        {
            return MoviePeopleDao.Instance.GetMoviePeoplesByMovieId(movieId);
        }
    }
}
