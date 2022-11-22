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
    public class FollowedMovieDao
    {
        private static FollowedMovieDao instance = null!;
        private static readonly object instanceLock = new object();

        public FollowedMovieDao()
        {
        }

        public static FollowedMovieDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new FollowedMovieDao();
                    return instance;
                }
            }
        }

        public IEnumerable<FollowedMovie> GetFollowedMovies()
        {
            IQueryable<FollowedMovie> FollowedMovies;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                FollowedMovies = from m in _context.FollowedMovies.Include(m => m.User).Include(m => m.Movie)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return FollowedMovies;
        }

        public async Task<FollowedMovie> GetFollowedMovieByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var FollowedMovies = await _context.FollowedMovies
                .Include(m => m.User).Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            return FollowedMovies!;
        }

        public async Task Add(FollowedMovie FollowedMovie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var FollowedMovies = await _context.FollowedMovies.FindAsync(FollowedMovie.Id);
                if (FollowedMovies == null)
                {
                    _context.Add(FollowedMovie);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(FollowedMovie FollowedMovie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(FollowedMovie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(FollowedMovie FollowedMovie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(FollowedMovie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
