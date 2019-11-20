using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PersonalList.Classes
{
    public class AEmployeeList: INotifyPropertyChanged
    {
        public AEmployeeList()
        {
            employeeList = new ObservableCollection<AEmployee>();
        }

        #region Логика

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="NewEmployee">новый сотрудник</param>
        /// <returns>true - если добавили, false - если такой сотрудник уже есть и не добавили</returns>
        public bool AddEmployee(AEmployee NewEmployee)
        {
            bool flag = false;
            if (!employeeList.Contains(NewEmployee))
            {
                employeeList.Add(NewEmployee);
                flag = true;
            }

            return flag;
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="RemoveEmployee">сотрудник для удаления</param>
        /// <returns>успешно или нет</returns>
        public bool RemoveEmployee(AEmployee RemoveEmployee)
        {

            bool flag = false;
            if (employeeList.IndexOf(RemoveEmployee) != -1)
            {
                employeeList.Remove(RemoveEmployee);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Изменить сотрудника
        /// </summary>
        /// <param name="CurrentDepartment">текущий сотрудник</param>
        /// <param name="AlterDepartment">измененный сотрдуник</param>
        /// <returns>успешно или нет</returns>
        public bool AlterEmployee(AEmployee CurrentEmployee, AEmployee AlterEmployee)
        {
            bool flag = false;
            if (CurrentEmployee != null && AlterEmployee != null)
            {
                int a = employeeList.IndexOf(CurrentEmployee);
                employeeList[a] = AlterEmployee;
                flag = true;
            }
            return flag;
        }

        public int CountEmployee => employeeList.Count;

        /// <summary>
        /// Индексатор класса
        /// </summary>
        public AEmployee this[int index]
        {
            get => !IsNullOrEmpty() ? employeeList[index] : null;
        }

        /// <summary>
        /// Проверка на наличие сотрудников в списке
        /// </summary>
        /// <returns>есть/нет</returns>
        private bool IsNullOrEmpty()
        {
            bool flag = true;

            if(employeeList != null)
            {
                if(employeeList.Count>0)
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
            foreach (AEmployee e in employeeList)
            {
               yield return (AEmployee)e;
            }
        }

        #endregion

        #region Данные

        public ObservableCollection<AEmployee> EmployeeList
        {
            get => this.employeeList;
            set
            {
                this.employeeList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.EmployeeList)));
            }
        }

        private ObservableCollection<AEmployee> employeeList;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
