using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListAppAPI.Services.Interfaces;
using ToDoListAppData.Model;
using ToDoListAppData.Repositories.Interfaces;
using ToDoListAppData.ViewModel;

namespace ToDoListAppAPI.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //public int Add(UserVM userVM)
        //{
        //    if (!string.IsNullOrWhiteSpace(userVM.Name) || !string.IsNullOrWhiteSpace(userVM.UserName))
        //    {
        //        var save = _userRepository.Add(userVM);
        //        if (save > 0)
        //        {
        //            return save;
        //        }
        //        return 0;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        public bool Login(UserVM userVM)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public int Edit(int Id, UserVM userVM)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Get(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
