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
    public class EpisodeDao
    {
        private static EpisodeDao instance = null;
        private static readonly object instanceLock = new object();

        public EpisodeDao()
        {
        }

        public static EpisodeDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EpisodeDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Episode  > GetEpisodes  ()
        {
            IQueryable<Episode>  episodes  ;
            try
            {
                MovieDbContext _context = new();
                 episodes  = from m in _context.Episodes.Include(e=>e.Movie)
                         select m;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return  episodes  ;
        }

        public async Task<Episode> GetEpisodeByIdAsync(string id)
        {
            MovieDbContext _context = new();
            var  episodes  = await _context.Episodes
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            return  episodes;
        }

        public async Task Add( Episode episode)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var  episodes  = await _context.Episodes.FindAsync(episode.Id);
                if ( episodes  == null)
                {
                    _context.Add(episode);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("The episode with the same name has already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Episode Episode)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Episodes.Update(Episode);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete( Episode Episode)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(Episode);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
