using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListAppData.ViewModel
{
    public class ToDoListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }
        public Nullable<DateTimeOffset> CompletedDate { get; set; }
    }
}
