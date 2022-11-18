using BusinessObject.Models;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public Task AddRole(Role Role)
        {
            return RoleDao.Instance.Add(Role);
        }

        public Task Delete(Role Role)
        {
            return RoleDao.Instance.Delete(Role);
        }

        public Task<Role> GetRoleByIdAsync(string id)
        {
            return RoleDao.Instance.GetRoleByIdAsync(id);
        }

        public IEnumerable<Role> GetRoles()
        {
            return RoleDao.Instance.GetRoles();
        }

        public IEnumerable<Role> GetRolesByPeopleIdAsync(string peopleId)
        {
            return RoleDao.Instance.GetRolesByPeopleIdAsync(peopleId);
        }

        public Task Update(Role Role)
        {
            return RoleDao.Instance.Update(Role);
        }
    }
}
