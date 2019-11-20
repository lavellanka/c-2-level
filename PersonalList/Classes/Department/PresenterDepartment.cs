using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PersonalList.Classes
{
    class PresenterDepartment
    {
        private ModelDepartment model;
        private IViewDepartment view;

        public PresenterDepartment(IViewDepartment View)
        {
            this.view = View;
            model = new ModelDepartment();
        }

        public void LoadADepartmentList()
        {
            model.LoadData();

            if (model.CurrentDepartmentList.CountDepartment > 0)
            {
                model.CurrentIndex = 0;
                var tempDepartment = model.CurrentDepartment;

                view.DepartmentD = tempDepartment.Department;
                view.DepartmentNew = tempDepartment.Department;
                view.DepartmentListD = model.CurrentDepartmentList;
            }
        }

        public void Add()
        {
            model.CurrentDepartmentList.AddDepartment(new ADepartment(view.DepartmentNew));
        }

        public void Remove()
        {
            ADepartment t = new ADepartment(view.DepartmentD);
            model.CurrentDepartmentList.RemoveDepartment(t);

            if (model.CurrentDepartmentList.CountDepartment < 1)
            {
                model.CurrentIndex = -1;
                view.DepartmentD = string.Empty;
            }
            else
            {
                model.CurrentIndex--;
                if (model.CurrentIndex < 0) model.CurrentIndex = 0;                
                view.DepartmentD = model.CurrentDepartment.Department;
            }
        }

        public void Alter()
        {
            model.CurrentDepartmentList.AlterDepartment(new ADepartment(view.DepartmentD), new ADepartment(view.DepartmentNew));
        }

        public void SaveADepartmentList()
        {
            model.SaveData();
        }

        /// <summary>
        /// Даем доступ к текущему департаменту для переменной из IVIEW
        /// </summary>
        public ADepartmentList CurrentDepartamentList
        {
            get => model.CurrentDepartmentList;
            set => model.CurrentDepartmentList = value;
        }

        /// <summary>
        /// Доступ к количеству департаментов. Нужен во избежание ошибок при нажатии на удаление при пустом списке
        /// </summary>
        public int CurrentIndex
        {
            get => model.CurrentIndex;
        }
    }
}