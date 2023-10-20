using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_unit_testing
{
    public class EventInfo<T>
    {
        public string Action { get; set; }
        public T? Item { get; set; }

        public EventInfo(string action, T? item)
        {
            Action = action;
            Item = item;
        }
    }
}
