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
    public class GenreDao
    {
        private static GenreDao instance = null;
        private static readonly object instanceLock = new object();

        public GenreDao()
        {
        }

        public static GenreDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new GenreDao();
                    return instance;
                }
            }
        }

        public IEnumerable<Genre> GetGenres()
        {
            IEnumerable<Genre> genres;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                genres = from m in _context.Genres.Include(m => m.MovieGenres)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return genres;
        }

        public async Task<Genre> GetGenreByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var genres = await _context.Genres
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if(genres == null)
            {
                throw new Exception("Genre doesn't exist!");
            }
            return genres;
        }
        public async Task<Genre> GetGenreByNameAsync(string name)
        {
            MovieDbContext _context = new MovieDbContext();
            var genres = await _context.Genres
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Name == name);
            return genres;
        }

        public async Task Add(Genre genre)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var genres = await _context.Genres.FindAsync(genre.Id);
                if (genres == null)
                {
                    _context.Add(genre);
                    _ = _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Genre genre)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(genre);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(Genre genre)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(genre);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Genre> GetGenresByMovieId(string movieId)
        {
            IEnumerable<Genre> genres;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                genres = _context.MovieGenres.Where(m => m.MovieId == movieId).Select(m=>m.Genre);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return genres;
        }
    }
}
