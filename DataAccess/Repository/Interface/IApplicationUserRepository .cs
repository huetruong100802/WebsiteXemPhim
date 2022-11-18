using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetApplicationUsers();
        Task<ApplicationUser> GetApplicationUserByIdAsync(string id);
        Task<ApplicationUser> GetApplicationUserByNameAsync(string name);
        Task AddApplicationUser(ApplicationUser ApplicationUser);
        Task Update(ApplicationUser ApplicationUser);
        Task Delete(ApplicationUser ApplicationUser);
    }
}
