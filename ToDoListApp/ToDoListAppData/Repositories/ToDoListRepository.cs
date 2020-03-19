using Dapper;
using Microsoft.Data.SqlClient;
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
            using (var conn = new SqlConnection(_connectionStrings.Value))
            {
                var procInsert = "SP_InsertToDoList";
                param.Add("@paramName", toDoListVM.Name);
                param.Add("@paramUserId", toDoListVM.userId);
                param.Add("@paramCreateDate", DateTimeOffset.Now.LocalDateTime);
                var insert = conn.Execute(procInsert, param, commandType: System.Data.CommandType.StoredProcedure);
                return insert;
            }
        }

        public bool Delete(int Id, string userId)
        {
            var getbyid = Get(Id, userId);
            if(getbyid == null)
            {
                return false;
            }
            else
            {
                using (var conn = new SqlConnection(_connectionStrings.Value))
                {
                    var procDelete = "SP_UpdateToDoList";
                    param.Add("@paramId", Id);
                    param.Add("@paramCompletedDate", null);
                    param.Add("@paramUserId", userId);
                    param.Add("@paramDeleteDate", DateTimeOffset.Now.LocalDateTime);
                    param.Add("@paramName", null);
                    param.Add("@paramUpdateDate", null);
                    var delete = Convert.ToBoolean(conn.Execute(procDelete, param, commandType: System.Data.CommandType.StoredProcedure));
                    return delete;
                }
            }
        }

        public int Edit(int Id, ToDoListVM toDoListVM)
        {
            using (var conn = new SqlConnection(_connectionStrings.Value))
            {
                var procUpdate = "SP_UpdateToDoList";
                param.Add("@paramId", Id);
                param.Add("@paramCompletedDate", null);
                param.Add("@paramUserId", toDoListVM.userId);
                param.Add("@paramDeleteDate", null);
                param.Add("@paramName", toDoListVM.Name);
                param.Add("@paramUpdateDate", DateTimeOffset.Now.LocalDateTime);
                var edit = conn.Execute(procUpdate, param, commandType: System.Data.CommandType.StoredProcedure);
                return edit;
            }
        }

        public IEnumerable<ToDoListVM> Get()
        {
            using (var conn = new SqlConnection(_connectionStrings.Value))
            {
                var procGet = "SP_GetToDoLists";
                param.Add("@paramId", null);
                param.Add("@paramUserId", null);
                var get = conn.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure);
                return get;
            }
        }

        public ToDoListVM Get(int Id, string userId)
        {
            using (var conn = new SqlConnection(_connectionStrings.Value))
            {
                var procGet = "SP_GetToDoLists";
                param.Add("@paramId", Id);
                param.Add("@paramUserId", userId);
                var getbyid = conn.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
                return getbyid;
            }
        }

        public IEnumerable<ToDoListVM> Get(string userId)
        {
            using (var conn = new SqlConnection(_connectionStrings.Value))
            {
                var procGet = "SP_GetToDoLists";
                param.Add("@paramId", null);
                param.Add("@paramUserId", userId);
                var getbyid = conn.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure);
                return getbyid;
            }
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
                using (var conn = new SqlConnection(_connectionStrings.Value))
                {
                    var procUpdateStatus = "SP_UpdateToDoList";
                    param.Add("@paramId", Id);
                    param.Add("@paramCompletedDate", DateTimeOffset.Now.LocalDateTime);
                    param.Add("@paramUserId", userId);
                    param.Add("@paramDeleteDate", null);
                    param.Add("@paramName", null);
                    param.Add("@paramUpdateDate", null);
                    var updatestatus = conn.Execute(procUpdateStatus, param, commandType: System.Data.CommandType.StoredProcedure);
                    return Convert.ToBoolean(updatestatus);
                }
            }
        }
    }
}
