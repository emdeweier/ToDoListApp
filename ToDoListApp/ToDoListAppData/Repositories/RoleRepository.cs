using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoListAppData.Model;
using ToDoListAppData.Repositories.Interfaces;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public IConfiguration _configuration;
        public readonly ConnectionStrings _connectionStrings;
        public RoleRepository(
            ConnectionStrings connectionStrings,
            IConfiguration configuration
            )
        {
            _configuration = configuration;
            _connectionStrings = connectionStrings;
        }
        DynamicParameters param = new DynamicParameters();
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
