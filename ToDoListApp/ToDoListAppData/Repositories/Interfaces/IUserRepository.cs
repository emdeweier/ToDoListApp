using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get();
        Task<IEnumerable<User>> Get(int Id);
        //int Add(UserVM userVM);
        bool Login(UserVM userVM);
        int Edit(int Id, UserVM userVM);
        bool Delete(int Id);
    }
}
