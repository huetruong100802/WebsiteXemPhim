using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StatusRepository : IStatusRepository
    {
        public Task AddStatus(Status Status)
        {
            return StatusDao.Instance.Add(Status);
        }

        public Task Delete(Status Status)
        {
            return StatusDao.Instance.Delete(Status);
        }

        public Task<Status> GetStatusByIdAsync(int id)
        {
            return StatusDao.Instance.GetStatusByIdAsync(id);
        }

        public Task<Status> GetStatusByNameAsync(string name)
        {
            return StatusDao.Instance.GetStatusByNameAsync(name);
        }

        public IEnumerable<Status> GetStatuses()
        {
            return StatusDao.Instance.GetStatuses();
        }

        public IEnumerable<Status> GetStatusesByMovieId(string movieId)
        {
            return StatusDao.Instance.GetStatusesByMovieId(movieId);
        }

        public Task Update(Status Status)
        {
            return StatusDao.Instance.Update(Status);
        }
    }
}
