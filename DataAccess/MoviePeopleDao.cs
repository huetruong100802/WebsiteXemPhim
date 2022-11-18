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
    public class MoviePeopleDao
    {
        private static MoviePeopleDao instance = null;
        private static readonly object instanceLock = new object();

        public MoviePeopleDao()
        {
        }

        public static MoviePeopleDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MoviePeopleDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MoviePeople> GetMoviePeoples()
        {
            IQueryable<MoviePeople> MoviePeoples;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                MoviePeoples = from m in _context.MoviePeoples
                               .Include(m => m.People).Include(m => m.Movie).Include(m=>m.Roles)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return MoviePeoples;
        }
        public IEnumerable<MoviePeople> GetMoviePeoplesByMovieId(string movieId)
        {
            IQueryable<MoviePeople> moviePeoples;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                moviePeoples = from m in _context.MoviePeoples
                               .Include(m => m.People).Include(m => m.Movie).Include(m => m.Roles)
                               where m.MovieId==movieId
                               select m;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return moviePeoples;
        }
        public async Task<MoviePeople> GetMoviePeopleByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var MoviePeoples = await _context.MoviePeoples
                .Include(m => m.People).Include(m => m.Movie).Include(m => m.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            return MoviePeoples!;
        }
        public async Task<MoviePeople> GetMoviePeopleByMovieIdAsync(string movieId)
        {
            MovieDbContext _context = new MovieDbContext();
            var MoviePeoples = await _context.MoviePeoples
                .Include(m => m.People).Include(m => m.Movie).Include(m => m.Roles)
                .FirstOrDefaultAsync(m => m.MovieId==movieId);
            return MoviePeoples;
        }
        public async Task<MoviePeople> GetMoviePeopleByRoleIdAsync(string roleId)
        {
            MovieDbContext _context = new MovieDbContext();
            var MoviePeoples = await _context.MoviePeoples
                .Include(m => m.People).Include(m => m.Movie).Include(m => m.Roles)
                .FirstOrDefaultAsync(m => m.RoleId==roleId);
            return MoviePeoples;
        }

        public async Task Add(MoviePeople MoviePeople)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var MoviePeoples = await _context.MoviePeoples.FindAsync(MoviePeople.Id);
                if (MoviePeoples == null)
                {
                    _context.Add(MoviePeople);
                    _ = _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(MoviePeople MoviePeople)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(MoviePeople);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(MoviePeople MoviePeople)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(MoviePeople);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteRange(List<MoviePeople> moviePeople)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.MoviePeoples.RemoveRange(moviePeople);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
