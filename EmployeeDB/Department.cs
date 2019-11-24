using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDB
{
    /// <summary>
    /// Класс департамента. Включает коллекцию сотрудников
    /// </summary>
    public class Department : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> EmpList;
        
        protected string _DepName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DepName {
            get => _DepName;
            set
            {
                if (value != string.Empty)
                {
                    _DepName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.DepName)));
                }
                else
                {
                    throw new ArgumentException("Имя департамента не может быть пустым.");
                }
            }
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Название департамента</param>
        public Department(string name)
        {
            DepName = name;
            EmpList = new ObservableCollection<Employee>();
        }

        public override bool Equals(object obj)
        {
            return (obj as Department)?.DepName.Equals(this.DepName) ?? false;
        }

        #region icoll
        //public int Count => EmpList.Count();

        //public bool IsReadOnly => false;

        //public Employee this[int index] { get => _EmpList[index]; set => _EmpList[index]=value; }

        //public void Add(Employee item)
        //{
        //    _EmpList.Add(item);
        //}

        //public void Clear()
        //{
        //    _EmpList.Clear();
        //}

        //public bool Contains(Employee item)
        //{
        //    return _EmpList.Contains(item);
        //}

        //public void CopyTo(Employee[] array, int arrayIndex)
        //{
        //    _EmpList.CopyTo(array,arrayIndex);
        //}

        //public IEnumerator<Employee> GetEnumerator()
        //{
        //    return _EmpList.GetEnumerator();
        //}

        //public bool Remove(Employee item)
        //{
        //    return _EmpList.Remove(item);
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return _EmpList.GetEnumerator();
        //}

        //public int IndexOf(Employee item)
        //{
        //    return _EmpList.IndexOf(item);
        //}

        //public void Insert(int index, Employee item)
        //{
        //    _EmpList.Insert(index,item);
        //}

        //public void RemoveAt(int index)
        //{
        //    _EmpList.RemoveAt(index);
        //}
        //public override string ToString()
        //{
        //    return _DepName;
        //}
        #endregion
    }
}