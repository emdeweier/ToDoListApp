using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Repositories.Interfaces
{
    public interface IToDoListRepository
    {
        IEnumerable<ToDoListVM> Get();
        ToDoListVM Get(int Id);
        int Add(ToDoListVM toDoListVM);
        int Edit(int Id, ToDoListVM toDoListVM);
        bool UpdateStatus(int Id, ToDoListVM toDoListVM);
        bool Delete(int Id);
    }
}
