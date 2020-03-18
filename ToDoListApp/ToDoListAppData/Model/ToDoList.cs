using System;
using System.Collections.Generic;
using System.Text;
using ToDoListAppData.Base;
using ToDoListAppData.ViewModel;

namespace ToDoListAppData.Model
{
    public class ToDoList : BaseModel
    {
        public string Name { get; set; }
        public User user { get; set; }
        public bool Status { get; set; }
        public Nullable<DateTimeOffset> CompletedDate { get; set; }
        public ToDoList() { }
        public ToDoList(ToDoListVM toDoListVM)
        {
            this.Name = toDoListVM.Name;
            this.user.Id = toDoListVM.userId;
            this.Status = false;
            this.IsDelete = false;
            this.CreateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Update(ToDoListVM toDoListVM)
        {
            this.Name = toDoListVM.Name;
            this.Status = false;
            this.UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Delete(ToDoListVM toDoListVM)
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}
