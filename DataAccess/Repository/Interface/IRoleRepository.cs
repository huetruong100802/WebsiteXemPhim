using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
        Task<Role> GetRoleByIdAsync(string id);
        IEnumerable<Role> GetRolesByPeopleIdAsync(string peopleId);
        Task AddRole(Role Role);
        Task Update(Role Role);
        Task Delete(Role Role);
    }
}
