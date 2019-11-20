using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalList.Classes
{
    class PresenterEmployee
    {
        private ModelEmployee model;
        private IViewEmployee view;

        public PresenterEmployee(IViewEmployee View)
        {
            this.view = View;
            model = new ModelEmployee();
        }

        public void LoadAEmployeeList()
        {
            model.LoadData();

            if (model.CurrentEmployeeList.CountEmployee > 0)
            {
                model.CurrentIndex = 0;
                var tempEmployee = model.CurrentEmployee;

                view.Surname = tempEmployee.Surname;
                view.Name = tempEmployee.Name;
                view.DepartmentE = tempEmployee.Department;
                view.EmployeeList = model.CurrentEmployeeList;
            }
        }

        public void Add()
        {
            model.CurrentEmployeeList.AddEmployee(new AEmployee(view.NewSurname, view.NewName, view.NewDepartment));
        }

        public void Remove()
        {
            AEmployee t = new AEmployee(view.Surname, view.Name, view.DepartmentE);
            model.CurrentEmployeeList.RemoveEmployee(t);

            if (model.CurrentEmployeeList.CountEmployee < 1)
            {
                model.CurrentIndex = -1;

                view.Surname = string.Empty;
                view.Name = string.Empty;
                view.DepartmentE = string.Empty;
            }
            else
            {
                model.CurrentIndex--;
                if (model.CurrentIndex < 0) model.CurrentIndex = 0;

                view.Surname = model.CurrentEmployee.Surname;
                view.Name = model.CurrentEmployee.Name;
                view.DepartmentE = model.CurrentEmployee.Department;
            }
        }

        public void Alter()
        {
            model.CurrentEmployee.Surname = view.NewSurname;
            model.CurrentEmployee.Name = view.NewName;
            model.CurrentEmployee.Department = view.NewDepartment;
        }

        public void SaveAEmployeeList()
        {
            model.SaveData();
        }

        public void Next()
        {
            if(model.CurrentEmployeeList.CountEmployee > 0)
            {
                if(model.CurrentIndex + 1 < model.CurrentEmployeeList.CountEmployee)
                {
                    model.CurrentIndex++;
                    view.Surname = model.CurrentEmployee.Surname;
                    view.Name = model.CurrentEmployee.Name;
                    view.DepartmentE = model.CurrentEmployee.Department;
                }
            }
        }

        public void Prev()
        {
            if (model.CurrentEmployeeList.CountEmployee > 0)
            {
                if (model.CurrentIndex - 1 > -1)
                {
                    model.CurrentIndex--;
                    view.Surname = model.CurrentEmployee.Surname;
                    view.Name = model.CurrentEmployee.Name;
                    view.DepartmentE = model.CurrentEmployee.Department;
                }
            }
        }

        public AEmployeeList CurrentEmployeeList
        {
            get => model.CurrentEmployeeList;
            set => model.CurrentEmployeeList = value;
        }
    }
}