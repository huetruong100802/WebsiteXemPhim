using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public Task AddApplicationUser(ApplicationUser ApplicationUser)
        {
            return ApplicationUserDao.Instance.Add(ApplicationUser);
        }

        public Task Delete(ApplicationUser ApplicationUser)
        {
            return ApplicationUserDao.Instance.Delete(ApplicationUser);
        }

        public Task<ApplicationUser> GetApplicationUserByIdAsync(string id)
        {
            return ApplicationUserDao.Instance.GetApplicationUserByIdAsync(id);
        }

        public Task<ApplicationUser> GetApplicationUserByNameAsync(string name)
        {
            return ApplicationUserDao.Instance.GetApplicationUserByNameAsync(name);
        }

        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            return ApplicationUserDao.Instance.GetApplicationUsers();
        }

        public Task Update(ApplicationUser ApplicationUser)
        {
            return ApplicationUserDao.Instance.Update(ApplicationUser);
        }
    }
}
