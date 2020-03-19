using System.Collections.Generic;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Repositories.Interfaces
{
    public interface IToDoListRepository
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
