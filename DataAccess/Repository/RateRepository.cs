using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RateRepository : IRateRepository
    {
        public Task AddRate(Rate Rate)
        {
            return RateDao.Instance.Add(Rate);
        }

        public Task Delete(Rate Rate)
        {
            return RateDao.Instance.Delete(Rate);
        }

        public Task<Rate> GetRateByIdAsync(string id)
        {
            return RateDao.Instance.GetRateByIdAsync(id);
        }

        public IEnumerable<Rate> GetRates()
        {
            return RateDao.Instance.GetRates();
        }

        public Task Update(Rate Rate)
        {
            return RateDao.Instance.Update(Rate);
        }
    }
}
