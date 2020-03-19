using System.Collections.Generic;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Services.Interfaces
{
    public interface IToDoListService
    {
        IEnumerable<ToDoListVM> Get();
        ToDoListVM Get(int Id, string userId);
        IEnumerable<ToDoListVM> Get(string userId);
        int Add(ToDoListVM toDoListVM);
        int Edit(int Id, ToDoListVM toDoListVM);
        bool UpdateStatus(int Id, string userId);
        bool Delete(int Id, string userId);
    }
}
