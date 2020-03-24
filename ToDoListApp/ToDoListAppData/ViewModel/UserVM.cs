using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListAppData.ViewModel
{
    public class UserVM : IdentityUser
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public bool TokenStatus { get; set; }
        public bool LockedStatus { get; set; }
        public string PIN { get; set; }
        public bool IsDelete { get; set; }
        public string RoleName { get; set; }
        public DateTimeOffset JoinDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }
    }
}
