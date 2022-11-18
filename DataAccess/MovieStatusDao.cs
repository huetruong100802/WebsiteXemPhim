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
    public class MovieStatusDao
    {
        private static MovieStatusDao instance = null;
        private static readonly object instanceLock = new object();

        public MovieStatusDao()
        {
        }

        public static MovieStatusDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MovieStatusDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MovieStatus> GetMovieStatuses()
        {
            IQueryable<MovieStatus> MovieStatuses;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                MovieStatuses = from m in _context.MovieStatuses
                               .Include(m => m.Movie).Include(m=>m.Status)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return MovieStatuses;
        }
        public IEnumerable<MovieStatus> GetMovieStatusesByMovieId(string movieId)
        {
            IQueryable<MovieStatus> MovieStatuses;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                MovieStatuses = from m in _context.MovieStatuses
                               .Include(m => m.Movie).Include(m=>m.Status)
                               where m.MovieId==movieId
                               select m;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return MovieStatuses;
        }
        public async Task<MovieStatus> GetMovieStatusByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var MovieStatuses = await _context.MovieStatuses
                .Include(m => m.Movie).Include(m=>m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            return MovieStatuses!;
        }
        public async Task<MovieStatus> GetMovieStatusByMovieIdAsync(string movieId)
        {
            MovieDbContext _context = new MovieDbContext();
            var MovieStatuses = await _context.MovieStatuses
                .Include(m => m.Movie).Include(m=>m.Status)
                .FirstOrDefaultAsync(m => m.MovieId==movieId);
            return MovieStatuses;
        }
        public async Task<MovieStatus> GetMovieStatusByStatusIdAsync(int statusId)
        {
            MovieDbContext _context = new MovieDbContext();
            var MovieStatuses = await _context.MovieStatuses
                .Include(m => m.Movie).Include(m=>m.Status)
                .FirstOrDefaultAsync(m => m.StatusId==statusId);
            return MovieStatuses;
        }

        public async Task Add(MovieStatus MovieStatus)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var MovieStatuses = await _context.MovieStatuses.FindAsync(MovieStatus.Id);
                if (MovieStatuses == null)
                {
                    _context.Add(MovieStatus);
                    _ = _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(MovieStatus MovieStatus)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(MovieStatus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(MovieStatus MovieStatus)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(MovieStatus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteRange(IEnumerable<MovieStatus> MovieStatus)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.MovieStatuses.RemoveRange(MovieStatus);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
