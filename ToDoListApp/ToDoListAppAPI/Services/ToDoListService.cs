using System.Collections.Generic;
using ToDoListAppAPI.Services.Interfaces;
using ToDoListAppData.Repositories.Interfaces;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Services
{
    public class ToDoListService : IToDoListService
    {
        IToDoListRepository _toDoListRepository;
        public ToDoListService(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        public int Add(ToDoListVM toDoListVM)
        {
            if (!string.IsNullOrWhiteSpace(toDoListVM.Name))
            {
                var save = _toDoListRepository.Add(toDoListVM);
                if (save > 0)
                {
                    return save;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int Id, string userId)
        {
            var delete = _toDoListRepository.Delete(Id, userId);
            if (delete == true)
            {
                return true;
            }
            return false;
        }

        public int Edit(int Id, ToDoListVM toDoListVM)
        {
            if (!string.IsNullOrWhiteSpace(toDoListVM.Name))
            {
                var edit = _toDoListRepository.Edit(Id, toDoListVM);
                if (edit > 0)
                {
                    return edit;
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<ToDoListVM> Get()
        {
            var todolist = _toDoListRepository.Get();
            if (todolist != null)
            {
                return todolist;
            }
            return null;
        }

        public ToDoListVM Get(int Id, string userId)
        {
            var todolist = _toDoListRepository.Get(Id, userId);
            if (todolist != null)
            {
                return todolist;
            }
            return null;
        }

        public IEnumerable<ToDoListVM> Get(string userId)
        {
            var todolist = _toDoListRepository.Get(userId);
            if (todolist != null)
            {
                return todolist;
            }
            return null;
        }

        public bool UpdateStatus(int Id, string userId)
        {
            var updatestatus = _toDoListRepository.UpdateStatus(Id, userId);
            if (updatestatus == true)
            {
                return true;
            }
            return false;
        }
    }
}
