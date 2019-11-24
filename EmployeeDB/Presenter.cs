using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace EmployeeDB
{

    class Presenter
    {
        public ObservableCollection<Department> ListOfDep;
        private IView view;
        private Employee editEmployee;
        private Department editDepartament;
        public Presenter(IView view)
        {
            this.view = view;
            ListOfDep = new ObservableCollection<Department>();
            ListOfDep.Add(new Department("Производственное управление"));
            ListOfDep.Add(new Department("ОКБ"));
            ListOfDep[0].EmpList.Add(new Employee("Александра", "Свиргстина", "Инженер", new DateTime(1992, 04, 03)));
            ListOfDep[1].EmpList.Add(new Employee("Анастасия", "Смирнова", "Инженер", new DateTime(1992, 04, 15)));
        }

        public ObservableCollection<Employee> EmployeeList
        {
            get=> view.CurrDep?.EmpList;
        }
        /// <summary>
        /// Добавление департамента
        /// </summary>
        public void AddDepartment()
        {
            try
            {
                    ListOfDep.Add(new Department(view.DepName));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ClearTextDep();
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        public void RemoveDepartment()
        {
            if (view.CurrDep != null)
            {
                view.CurrDep.EmpList.Clear();
                ListOfDep.Remove(view.CurrDep);
            }
        }
        /// <summary>
        /// Переименование департамента
        /// </summary>
        public void RenameDepartment()
        {
            try
            {
                if (view.CurrDep != null)
                    view.CurrDep.DepName = view.DepName;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ClearTextDep();
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <returns>Истина, если сотрудник добавлен</returns>
        public bool AddEmployee()
        {
            bool AddSuc = true;
            try
            {
                view.EditDep?.EmpList?.Add(new Employee(view.Name, view.Surname, view.Position, view.Birthday));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                AddSuc = false;
            }
            if (AddSuc) ClearTextEmployee();
            return AddSuc;
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        public void RemoveEmployee()
        {
            view.CurrDep?.EmpList?.Remove(view.CurrEmployee);
        }
        /// <summary>
        /// Очистка поля ввода имени департамента
        /// </summary>
        private void ClearTextDep()
        {
            view.DepName = string.Empty;
        }
        /// <summary>
        /// Выбор сотрудника для редактирования
        /// </summary>
        public void SelectEmployee()
        {
            if (view.CurrEmployee != null)
            {
                editEmployee = view.CurrEmployee;
                editDepartament = view.CurrDep;
                view.Name = editEmployee.name;
                view.Surname = editEmployee.surname;
                view.Position = editEmployee.position;
                view.Birthday = editEmployee.birthday;
                view.EditDep = view.CurrDep;
            }
        }
        /// <summary>
        /// Сохранение отредактированного сотрудника
        /// </summary>
        public bool SaveEmployee()
        {
            bool SaveSuc = true;
            if (view.EditDep != editDepartament) //Сменился ли департамент
            {
                SaveSuc = AddEmployee();
                if (SaveSuc)
                {
                    editDepartament.EmpList.Remove(editEmployee);
                    ClearTextEmployee();
                }
            } else
            {
                try
                {
                    editEmployee.name = view.Name;
                    editEmployee.surname = view.Surname;
                    editEmployee.birthday = view.Birthday;
                    editEmployee.position = view.Position;
                    ClearTextEmployee();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    SaveSuc = false;
                }
            }
            return SaveSuc;
            
        }
        private void ClearTextEmployee()
        {
            view.Name = string.Empty;
            view.Surname = string.Empty;
            view.Position = string.Empty;
            view.Birthday = DateTime.Now;
            view.EditDep = null;
        }
    }
}