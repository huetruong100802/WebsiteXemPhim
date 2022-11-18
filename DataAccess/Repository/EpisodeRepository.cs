using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EpisodeRepository : IEpisodeRepository
    {
        public Task AddEpisode(Episode Episode)
        {
            return EpisodeDao.Instance.Add(Episode);
        }

        public Task Delete(Episode Episode)
        {
            return EpisodeDao.Instance.Delete(Episode);
        }

        public Task<Episode> GetEpisodeByIdAsync(string id)
        {
            return EpisodeDao.Instance.GetEpisodeByIdAsync(id);
        }

        public IEnumerable<Episode> GetEpisodes()
        {
            return EpisodeDao.Instance.GetEpisodes();
        }

        public Task Update(Episode Episode)
        {
            return EpisodeDao.Instance.Update(Episode);
        }
    }
}
