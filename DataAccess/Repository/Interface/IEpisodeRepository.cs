using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IEpisodeRepository
    {
        IEnumerable<Episode> GetEpisodes();
        Task<Episode> GetEpisodeByIdAsync(string id);
        Task AddEpisode(Episode Episode);
        Task Update(Episode Episode);
        Task Delete(Episode Episode);
    }
}
