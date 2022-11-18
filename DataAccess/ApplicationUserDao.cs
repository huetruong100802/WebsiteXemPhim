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
    public class ApplicationUserDao
    {
        private static ApplicationUserDao instance = null!;
        private static readonly object instanceLock = new();

        public ApplicationUserDao()
        {
        }

        public static ApplicationUserDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationUserDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            IQueryable<ApplicationUser> ApplicationUsers;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                ApplicationUsers = from m in _context.ApplicationUsers.Include(m=>m.Rates).Include(m=>m.Comments)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ApplicationUsers;
        }

        public async Task<ApplicationUser> GetApplicationUserByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var ApplicationUsers = await _context.ApplicationUsers
                .Include(m=>m.Rates).Include(m=>m.Comments)
                .SingleOrDefaultAsync(m => m.Id == id);
            return ApplicationUsers;
        }
        public async Task<ApplicationUser> GetApplicationUserByNameAsync(string name)
        {
            MovieDbContext _context = new MovieDbContext();
            var ApplicationUsers = await _context.ApplicationUsers
                .Include(m => m.Rates).Include(m => m.Comments)
                .FirstOrDefaultAsync(m => m.UserName==name);
            return ApplicationUsers;
        }
        public async Task Add(ApplicationUser ApplicationUser)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var ApplicationUsers = await _context.ApplicationUsers.FindAsync(ApplicationUser.Id);
                if (ApplicationUsers == null)
                {
                    _context.Add(ApplicationUser);
                    _ = _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(ApplicationUser ApplicationUser)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(ApplicationUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(ApplicationUser ApplicationUser)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(ApplicationUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
