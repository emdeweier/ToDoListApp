﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Services.Interfaces
{
    public interface IToDoListService
    {
        IEnumerable<ToDoListVM> Get();
        ToDoListVM Get(int Id, string userId);
        UserVM GetDataUser(string userId);
        IEnumerable<ToDoListVM> GetToDoLists(string userId);
        //IEnumerable<ToDoListVM> Get(string userId);
        Task<ToDoListVM> Get(string userId, int status, string keyword, int page, int pageSize);
        int Add(ToDoListVM toDoListVM);
        int Edit(int Id, ToDoListVM toDoListVM);
        bool UpdateStatus(int Id, string userId);
        bool Delete(int Id, string userId);
    }
}
