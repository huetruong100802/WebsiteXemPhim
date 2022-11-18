using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        public Task AddPeople(People People)
        {
            return PeopleDao.Instance.Add(People);
        }

        public Task Delete(People People)
        {
            return PeopleDao.Instance.Delete(People);
        }

        public Task<People> GetPeopleByIdAsync(string id)
        {
            return PeopleDao.Instance.GetPeopleByIdAsync(id);
        }

        public IEnumerable<People> GetPeopleByMovieId(string movieId)
        {
            return PeopleDao.Instance.GetPeopleByMovieId(movieId);
        }

        public IEnumerable<People> GetPeoples()
        {
            return PeopleDao.Instance.GetPeoples();
        }

        public bool PeopleIsInMovie(Movie movie, string peopleName)
        {
            return PeopleDao.Instance.PeopleIsInMovie(movie, peopleName);
        }

        public bool PeopleIsInRole(Role role, string peopleName)
        {
            return PeopleDao.Instance.PeopleIsInRole(role, peopleName);
        }

        public Task Update(People People)
        {
            return PeopleDao.Instance.Update(People);
        }
    }
}
