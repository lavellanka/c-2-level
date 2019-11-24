using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDB
{
        public interface IView
        {
            string Name { get; set; }
            string Surname { get; set; }
            DateTime Birthday { get; set; }
            string Position { get; set; }
            string DepName { get; set; }
            Department CurrDep { get; }
            Department EditDep { get; set; }
            Employee CurrEmployee { get; }
        }
}