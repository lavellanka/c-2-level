using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PersonalList.Classes
{
    public class ADepartmentList : IEnumerable, INotifyPropertyChanged
    {
        public ADepartmentList()
        {
            departmentList = new ObservableCollection<ADepartment>();
        }

        #region Логика

        /// <summary>
        /// Добавить отдел
        /// </summary>
        public bool AddDepartment(ADepartment NewDepartment)
        {
            bool flag = false;
            if (!departmentList.Contains(NewDepartment))
            {
                departmentList.Add(NewDepartment);
                flag = true;
            }

            return flag;
        }

        /// <summary>
        /// Удалить отдел
        /// </summary>
        public bool RemoveDepartment(ADepartment RemoveDepartment)
        {

            bool flag = false;
            if (departmentList.IndexOf(RemoveDepartment) != -1)
            {
                departmentList.Remove(RemoveDepartment);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Изменить отдел
        /// </summary>
        /// <param name="CurrentDepartment">текущий отдел</param>
        /// <param name="AlterDepartment">измененный отдел</param>
        /// <returns>успешно или нет</returns>
        public bool AlterDepartment(ADepartment CurrentDepartment, ADepartment AlterDepartment)
        {
            bool flag = false;
            if (CurrentDepartment != null && AlterDepartment != null)
            {
                int a = departmentList.IndexOf(CurrentDepartment);
                departmentList[a] = AlterDepartment;                
                flag = true;
            }
            return flag;
        }

        public int CountDepartment => departmentList.Count;

        /// <summary>
        /// Индексатор класса
        /// </summary>
        public ADepartment this[int index]
        {
            get => !IsNullOrEmpty() ? departmentList[index] : null;
        }

        /// <summary>
        /// Проверка на наличие отдела в списке
        /// </summary>
        /// <returns>есть/нет</returns>
        private bool IsNullOrEmpty()
        {
            bool flag = true;

            if(departmentList != null)
            {
                if(departmentList.Count>0)
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// Нумератор, чтобы можно было пройтись циклом foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (ADepartment e in departmentList)
            {
               yield return (ADepartment)e;
            }
        }
        #endregion


        #region Данные

        public ObservableCollection<ADepartment> DepartmentList
        {
            get => this.departmentList;
            set
            {
                this.departmentList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.DepartmentList)));
            }
        }

        private ObservableCollection<ADepartment> departmentList;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
