using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAppData.Model;
using ToDoListAppData.Repositories.Interfaces;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly ConnectionStrings _connectionStrings;
        public UserRepository(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }
        DynamicParameters param = new DynamicParameters();

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get(int Id)
        {
            throw new NotImplementedException();
        }

        //public int Add(UserVM userVM)
        //{
        //    using (var conn = new SqlConnection(_connectionStrings.Value))
        //    {
        //        var procInsert = "SP_InsertUser";
        //        param.Add("@paramName", userVM.Name);
        //        param.Add("@paramUserName", userVM.UserName);
        //        param.Add("@paramPassword", userVM.PasswordHash);
        //        param.Add("@paramCreateDate", DateTimeOffset.Now.LocalDateTime);
        //        var insert = conn.Execute(procInsert, param, commandType: System.Data.CommandType.StoredProcedure);
        //        return insert;
        //    }
        //}

        public bool Login(UserVM userVM)
        {
            throw new NotImplementedException();
        }

        public int Edit(int Id, UserVM userVM)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
