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
    public class StatusDao
    {
        private static StatusDao instance = null!;
        private static readonly object instanceLock = new object();

        public StatusDao()
        {
        }

        public static StatusDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new StatusDao();
                    return instance;
                }
            }
        }

        public IEnumerable<Status> GetStatuses()
        {
            IEnumerable<Status> Statuses;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                Statuses = from m in _context.Statuses.Include(m => m.MovieStatuses)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Statuses;
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            MovieDbContext _context = new MovieDbContext();
            var Statuses = await _context.Statuses
                .Include(m => m.MovieStatuses)
                .FirstOrDefaultAsync(m => m.Id == id);
            if(Statuses == null)
            {
                throw new Exception("Status doesn't exist!");
            }
            return Statuses;
        }
        public async Task<Status> GetStatusByNameAsync(string name)
        {
            MovieDbContext _context = new MovieDbContext();
            var Statuses = await _context.Statuses
                .Include(m => m.MovieStatuses)
                .FirstOrDefaultAsync(m => m.Name == name);
            return Statuses;
        }

        public async Task Add(Status Status)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var Statuses = await _context.Statuses.FindAsync(Status.Id);
                if (Statuses == null)
                {
                    _context.Add(Status);
                    _ = _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Status Status)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(Status);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(Status Status)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(Status);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Status> GetStatusesByMovieId(string movieId)
        {
            IEnumerable<Status> Statuses;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                Statuses = _context.MovieStatuses.Where(m => m.MovieId == movieId).Select(m=>m.Status);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Statuses;
        }
    }
}
