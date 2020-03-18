using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListAppData
{
    public sealed class ConnectionStrings
    {
        public ConnectionStrings(string value) => Value = value;

        public string Value { get; }
    }
}
