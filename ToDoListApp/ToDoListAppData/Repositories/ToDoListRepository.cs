using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAppData.Model;
using ToDoListAppData.Repositories.Interfaces;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        public readonly ConnectionStrings _connectionStrings;
        public ToDoListRepository(ConnectionStrings connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }
        DynamicParameters param = new DynamicParameters();
        public int Add(ToDoListVM toDoListVM)
        {
            var procInsert = "SP_InsertToDoList";
            param.Add("@paramName", toDoListVM.Name);
            param.Add("@paramUserId", toDoListVM.userId);
            param.Add("@paramCreateDate", DateTimeOffset.Now.LocalDateTime);
            var insert = _connectionStrings.Connections.Execute(procInsert, param, commandType: System.Data.CommandType.StoredProcedure);
            return insert;
        }

        public bool Delete(int Id, string userId)
        {
            var getbyid = Get(Id, userId);
            if (getbyid == null)
            {
                return false;
            }
            else
            {
                var procDelete = "SP_UpdateToDoList";
                param.Add("@paramId", Id);
                param.Add("@paramCompletedDate", null);
                param.Add("@paramUserId", userId);
                param.Add("@paramDeleteDate", DateTimeOffset.Now.LocalDateTime);
                param.Add("@paramName", null);
                param.Add("@paramUpdateDate", null);
                var delete = Convert.ToBoolean(_connectionStrings.Connections.Execute(procDelete, param, commandType: System.Data.CommandType.StoredProcedure));
                return delete;
            }
        }

        public int Edit(int Id, ToDoListVM toDoListVM)
        {
            var procUpdate = "SP_UpdateToDoList";
            param.Add("@paramId", Id);
            param.Add("@paramCompletedDate", null);
            param.Add("@paramUserId", toDoListVM.userId);
            param.Add("@paramDeleteDate", null);
            param.Add("@paramName", toDoListVM.Name);
            param.Add("@paramUpdateDate", DateTimeOffset.Now.LocalDateTime);
            var edit = _connectionStrings.Connections.Execute(procUpdate, param, commandType: System.Data.CommandType.StoredProcedure);
            return edit;
        }

        public IEnumerable<ToDoListVM> Get()
        {
            var procGet = "SP_GetToDoLists";
            param.Add("@paramId", null);
            param.Add("@paramUserId", null);
            var get = _connectionStrings.Connections.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure);
            return get;
        }

        public ToDoListVM Get(int Id, string userId)
        {
            var procGet = "SP_GetToDoLists";
            param.Add("@paramId", Id);
            param.Add("@paramUserId", userId);
            var getbyid = _connectionStrings.Connections.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return getbyid;
        }

        public UserVM GetDataUser(string userId)
        {
            var procGet = "SP_GetDataUser";
            param.Add("@paramUserId", userId);
            var getbyuser = _connectionStrings.Connections.Query<UserVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return getbyuser;
        }

        public IEnumerable<ToDoListVM> GetToDoLists(string userId)
        {
            var procGet = "SP_GetToDoLists";
            param.Add("@paramId", null);
            param.Add("@paramUserId", userId);
            var get = _connectionStrings.Connections.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure);
            return get;
        }

        //public IEnumerable<ToDoListVM> Get(string userId)
        //{
        //    using (var conn = new SqlConnection(_connectionStrings.Value))
        //    {
        //        var procGet = "SP_GetToDoLists";
        //        param.Add("@paramId", null);
        //        param.Add("@paramUserId", userId);
        //        var getbyid = conn.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure);
        //        return getbyid;
        //    }
        //}

        public async Task<ToDoListVM> Get(string uid, int status, string keyword, int page, int size)
        {
            var procGet = "SP_ToDoListData";
            param.Add("@paramPageNumber", page);
            param.Add("@paramPageSize", size);
            param.Add("@paramUserId", uid);
            param.Add("@paramStatus", status);
            param.Add("@paramKeyword", keyword);
            param.Add("@length", DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@filterLength", DbType.Int32, direction: ParameterDirection.Output);
            var result = new ToDoListVM();
            result.data = await _connectionStrings.Connections.QueryAsync<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure);
            result.length = param.Get<int>("@length");
            result.filterLength = param.Get<int>("@filterLength");
            return result;
        }

        public bool UpdateStatus(int Id, string userId)
        {
            var getbyid = Get(Id, userId);
            if (getbyid == null)
            {
                return false;
            }
            else
            {
                var procUpdateStatus = "SP_UpdateToDoList";
                param.Add("@paramId", Id);
                param.Add("@paramCompletedDate", DateTimeOffset.Now.LocalDateTime);
                param.Add("@paramUserId", userId);
                param.Add("@paramDeleteDate", null);
                param.Add("@paramName", null);
                param.Add("@paramUpdateDate", null);
                var updatestatus = _connectionStrings.Connections.Execute(procUpdateStatus, param, commandType: System.Data.CommandType.StoredProcedure);
                return Convert.ToBoolean(updatestatus);
            }
        }
    }
}
