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
    public class MovieGenreDao
    {
        private static MovieGenreDao instance = null;
        private static readonly object instanceLock = new object();

        public MovieGenreDao()
        {
        }

        public static MovieGenreDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MovieGenreDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<MovieGenre> GetMovieGenres()
        {
            IQueryable<MovieGenre> movieGenre;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                movieGenre = from m in _context.MovieGenres.Include(m => m.Movie).Include(m=>m.Genre)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movieGenre;
        }

        public async Task<MovieGenre> GetMovieGenreByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var movieGenre = await _context.MovieGenres
                .Include(m => m.Movie).Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movieGenre!;
        }

        public async Task Add(MovieGenre movieGenre)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var movieGenres = await _context.MovieGenres.FindAsync(movieGenre.Id);
                if (movieGenres == null)
                {
                    _context.Add(movieGenre);
                    _ = _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(MovieGenre movieGenre)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(movieGenre);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(MovieGenre movieGenre)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(movieGenre);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteRange(List<MovieGenre> movieGenre)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.MovieGenres.RemoveRange(movieGenre);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<MovieGenre> GetMovieGenresByMovieId(string movieId)
        {
            IQueryable<MovieGenre> movieGenre;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                movieGenre = from m in _context.MovieGenres.Include(m => m.Movie).Include(m => m.Genre)
                             where m.MovieId == movieId
                             select m;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movieGenre;
        }
    }
}
