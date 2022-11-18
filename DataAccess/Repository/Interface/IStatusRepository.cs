using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IStatusRepository
    {
        IEnumerable<Status> GetStatuses();
        Task<Status> GetStatusByIdAsync(int id);
        Task<Status> GetStatusByNameAsync(string name);
        Task AddStatus(Status Status);
        Task Update(Status Status);
        Task Delete(Status Status);
        IEnumerable<Status> GetStatusesByMovieId(string movieId);
    }
}
