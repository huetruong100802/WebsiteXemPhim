using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PeopleDao
    {
        private static PeopleDao instance = null!;
        private static readonly object instanceLock = new object();

        public PeopleDao()
        {
        }

        public static PeopleDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PeopleDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<People> GetPeoples()
        {
            IQueryable<People> Peoples;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                Peoples = from m in _context.Peoples.Include(m => m.MoviePeoples)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Peoples;
        }

        public async Task<People> GetPeopleByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var Peoples = await _context.Peoples
                .Include(m => m.MoviePeoples)
                .FirstOrDefaultAsync(m => m.Id == id);
            return Peoples;
        }
        public IEnumerable<People> GetPeopleByMovieId(string movieId)
        {
            MovieDbContext _context = new MovieDbContext();
            var Peoples = _context.MoviePeoples
                .Where(m => m.MovieId == movieId).Select(m=>m.People);
            return Peoples;
        }
        public async Task Add(People People)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var Peoples = await _context.Peoples.FindAsync(People.Id);
                if (Peoples == null)
                {
                    _context.Add(People);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(People People)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(People);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(People People)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(People);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool PeopleIsInMovie(Movie movie, string peopleName)
        {
            MovieDbContext _context = new MovieDbContext();
            var moviePeople = from m in _context.MoviePeoples.Include(m => m.Movie).Include(m => m.People)
                              where m.Movie == movie && m.People.Name == peopleName
                              select m;
            return moviePeople.Any();
        }
        public bool PeopleIsInRole(Role role, string peopleName)
        {
            MovieDbContext _context = new MovieDbContext();
            var moviePeople = from m in _context.MoviePeoples.Include(m => m.People).Include(m=>m.Roles)
                              where m.Roles==role && m.People.Name == peopleName
                              select m;
            return moviePeople.Any();
        }
    }
}
