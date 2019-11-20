using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;

namespace PersonalList.Classes
{
    public interface IViewDepartment
    {
        string DepartmentNew { get; set; } //новый отдел
        string DepartmentD { get; set; } //выбранный отдел
        ADepartmentList DepartmentListD { get; set; } //список отделов
    }
}
