using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> Get();
        Role Get(string Id);
    }
}
