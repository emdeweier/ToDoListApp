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

        public bool Delete(int Id)
        {
            var getbyid = Get(Id);
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
                    param.Add("@paramName", null);
                    param.Add("@paramStatus", null);
                    param.Add("@paramUpdateDate", null);
                    param.Add("@paramCompletedDate", null);
                    param.Add("@paramDeleteDate", DateTimeOffset.Now.LocalDateTime);
                    var delete = conn.Execute(procDelete, param, commandType: System.Data.CommandType.StoredProcedure);
                    return Convert.ToBoolean(delete);
                }
            }
        }

        public int Edit(int Id, ToDoListVM toDoListVM)
        {
            using (var conn = new SqlConnection(_connectionStrings.Value))
            {
                var procUpdate = "SP_UpdateToDoList";
                param.Add("@paramId", Id);
                param.Add("@paramName", toDoListVM.Name);
                param.Add("@paramStatus", null);
                param.Add("@paramUpdateDate", DateTimeOffset.Now.LocalDateTime);
                param.Add("@paramCompletedDate", null);
                param.Add("@paramDeleteDate", null);
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
                var get = conn.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure);
                return get;
            }
        }

        public ToDoListVM Get(int Id)
        {
            using (var conn = new SqlConnection(_connectionStrings.Value))
            {
                var procGet = "SP_GetToDoLists";
                param.Add("@paramId", Id);
                var getbyid = conn.Query<ToDoListVM>(procGet, param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
                return getbyid;
            }
        }

        public bool UpdateStatus(int Id, ToDoListVM toDoListVM)
        {
            var getbyid = Get(Id);
            if (getbyid == null)
            {
                return false;
            }
            else
            {
                using (var conn = new SqlConnection(_connectionStrings.Value))
                {
                    var procDelete = "SP_UpdateToDoList";
                    param.Add("@paramId", Id);
                    param.Add("@paramName", null);
                    param.Add("@paramStatus", toDoListVM.Status);
                    param.Add("@paramUpdateDate", null);
                    param.Add("@paramCompletedDate", DateTimeOffset.Now.LocalDateTime);
                    param.Add("@paramDeleteDate", null);
                    var delete = conn.Execute(procDelete, param, commandType: System.Data.CommandType.StoredProcedure);
                    return Convert.ToBoolean(delete);
                }
            }
        }
    }
}
