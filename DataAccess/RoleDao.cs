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
    public class RoleDao
    {
        private static RoleDao instance = null;
        private static readonly object instanceLock = new object();

        public RoleDao()
        {
        }

        public static RoleDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            IQueryable<Role> Roles;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                Roles = from m in _context.Roles.Include(m => m.MoviePeoples)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Roles;
        }

        public async Task<Role> GetRoleByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var Roles = await _context.Roles
                .Include(m => m.MoviePeoples)
                .FirstOrDefaultAsync(m => m.Id == id);
            return Roles;
        }
        public IEnumerable<Role> GetRolesByPeopleIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var Roles = from moviePeople in _context.MoviePeoples
                        where moviePeople.PeopleId == id
                        select moviePeople.Roles;
            return Roles;
        }

        public async Task Add(Role Role)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var Roles = await _context.Roles.FindAsync(Role.Id);
                if (Roles == null)
                {
                    _context.Add(Role);
                    _ = _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Role Role)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(Role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(Role Role)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(Role);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
