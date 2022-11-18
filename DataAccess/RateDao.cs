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
    public class RateDao
    {
        private static RateDao instance = null!;
        private static readonly object instanceLock = new object();

        public RateDao()
        {
        }

        public static RateDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RateDao();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Rate> GetRates()
        {
            IQueryable<Rate> rates;
            try
            {
                MovieDbContext _context = new MovieDbContext();
                rates = from m in _context.Rates.Include(m => m.User).Include(m => m.Movie)
                         select m;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rates;
        }

        public async Task<Rate> GetRateByIdAsync(string id)
        {
            MovieDbContext _context = new MovieDbContext();
            var rates = await _context.Rates
                .Include(m => m.User).Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            return rates;
        }

        public async Task Add(Rate rate)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                var rates = await _context.Rates.FindAsync(rate.Id);
                if (rates == null)
                {
                    _context.Add(rate);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Rate rate)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Update(rate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Delete(Rate rate)
        {
            try
            {
                MovieDbContext _context = new MovieDbContext();
                _context.Remove(rate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
