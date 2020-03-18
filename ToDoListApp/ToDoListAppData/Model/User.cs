using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ToDoListAppData.Base;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Model
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public bool TokenStatus { get; set; }
        public bool LockedStatus { get; set; }
        public string PIN { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset JoinDate { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }
    }
}
