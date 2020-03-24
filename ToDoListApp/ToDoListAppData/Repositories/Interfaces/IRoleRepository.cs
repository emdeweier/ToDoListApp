using System;
using System.Collections.Generic;
using System.Text;
using ToDoListAppData.Model;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> Get();
        Role Get(string Id);
    }
}
