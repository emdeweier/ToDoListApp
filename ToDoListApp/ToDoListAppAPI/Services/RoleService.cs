using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAppAPI.Services.Interfaces;
using ToDoListAppData.Model;
using ToDoListAppData.Repositories.Interfaces;

namespace ToDoListAppAPI.Services
{
    public class RoleService : IRoleService
    {
        IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> Get()
        {
            throw new NotImplementedException();
        }

        public Role Get(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
