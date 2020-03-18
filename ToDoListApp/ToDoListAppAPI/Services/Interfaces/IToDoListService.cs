using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Services.Interfaces
{
    public interface IToDoListService
    {
        IEnumerable<ToDoListVM> Get();
        ToDoListVM Get(int Id);
        int Add(ToDoListVM toDoListVM);
        int Edit(int Id, ToDoListVM toDoListVM);
        bool UpdateStatus(int Id, ToDoListVM toDoListVM);
        bool Delete(int Id);
    }
}
