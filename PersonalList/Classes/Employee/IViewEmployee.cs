using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalList.Classes
{
    public interface IViewEmployee
    {
        string Surname { get; set; }
        string Name { get; set; }
        string DepartmentE { get; set; } 
        AEmployeeList EmployeeList { get; set; }

        string NewSurname { get; set; }
        string NewName { get; set; }
        string NewDepartment { get; set; } 
    }
}
