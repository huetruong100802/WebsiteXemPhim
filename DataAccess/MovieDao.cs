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
    public class MovieDao
    {
        private static MovieDao instance = null!;
        private static readonly object instanceLock = new object();

        public MovieDao()
        {
        }

        public static MovieDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new MovieDao();
                    return instance;
                }
            }
        }

        public IEnumerable<Movie> GetMovies()
        {
            IQueryable<Movie> movies;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                movies = from m in _context.Movies
                         .Include(m => m.MovieGenres).Include(m => m.Episodes).Include(m=>m.MovieStatuses)
                         select m;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movies;
        }
        public IEnumerable<Movie> GetMoviesByGenre(string genreName)
        {
            List<Movie> movies = new();
            try
            {
                MovieDbContext _context = new MovieDbContext();
                foreach (var movie in GetMovies())
                {
                    if (MovieIsInGenre(movie, genreName))
                    {
                        movies.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movies;
        }
        public IEnumerable<Movie> GetMoviesByKeyWord(string keyWord)
        {
            IEnumerable<Movie> movies;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                movies = movies = from m in _context.Movies
                                  .Include(m => m.MovieGenres).Include(m => m.Episodes).Include(m => m.MovieStatuses)
                                  where m.Title!.Contains(keyWord!)
                                  select m;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movies;
        }
        public async Task<Movie> GetMovieByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var movies = await _context.Movies
                .Include(m => m.MovieGenres).Include(m => m.Episodes).Include(m => m.Rates).Include(m=>m.MovieStatuses)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movies!;
        }

        public async Task Add(Movie movie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var movies = await _context.Movies.FindAsync(movie.Id);
                if (movies == null)
                {
                    _context.Add(movie);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Movie movie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(Movie movie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Hide(Movie movie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                movie.Deleted = true;
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Unhide(Movie movie)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                movie.Deleted = false;
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool MovieIsInGenre(Movie movie, string genreName)
        {
            MovieDbContext _context = new MovieDbContext();
            var movieGenre = from m in _context.MovieGenres.Include(m => m.Movie).Include(m => m.Genre)
                             where m.Movie == movie && m.Genre.Name == genreName
                             select m;
            return movieGenre.Any();
        }
        public bool MovieIsInStatus(Movie movie, string statusName)
        {
            MovieDbContext _context = new MovieDbContext();
            var movieGenre = from m in _context.MovieStatuses.Include(m => m.Movie).Include(m => m.Status)
                             where m.Movie == movie && m.Status.Name == statusName
                             select m;
            return movieGenre.Any();
        }
    }
}
