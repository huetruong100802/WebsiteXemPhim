using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IPeopleRepository
    {
        IEnumerable<People> GetPeoples();
        Task<People> GetPeopleByIdAsync(string id);
        bool PeopleIsInMovie(Movie movie, string peopleName);
        bool PeopleIsInRole(Role role, string peopleName);
        IEnumerable<People> GetPeopleByMovieId(string movieId);
        Task AddPeople(People People);
        Task Update(People People);
        Task Delete(People People);
    }
}
