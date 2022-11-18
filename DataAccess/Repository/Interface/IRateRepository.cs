using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IRateRepository
    {
        IEnumerable<Rate> GetRates();
        Task<Rate> GetRateByIdAsync(string id);
        Task AddRate(Rate Rate);
        Task Update(Rate Rate);
        Task Delete(Rate Rate);
    }
}
