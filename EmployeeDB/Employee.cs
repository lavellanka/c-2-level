using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace EmployeeDB
{
    /// <summary>
    /// Класс работника
    /// </summary>
    [Serializable]
    public class Employee : INotifyPropertyChanged
    {
        /// <summary>
        /// Имя
        /// </summary>
        protected string _name;

        public string name
        {
            get => _name;
            set
            {
                if (value == string.Empty || value == null)
                    throw new ArgumentNullException("Имя не может быть пустым.");
                else
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.name)));
                }
            }
        }
        /// <summary>
        /// Фамилия
        /// </summary>
        protected string _surname { get; set; }
        public string surname
        {
            get => _surname;
            set
            {
                if (value == string.Empty || value == null)
                    throw new ArgumentNullException("Фамилия не может быть пустой.");
                else
                {
                    _surname = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.surname)));
                }
            }
        }
        /// <summary>
        /// Уникальный номер сотрудника
        /// </summary>
        public int ID { get; private set; }
        protected static int LastID = 0;
        /// <summary>
        /// Должность
        /// </summary>
        protected string _position;

        public event PropertyChangedEventHandler PropertyChanged;

        public string position {
            set {
                if (value == string.Empty)
                {
                    throw new ArgumentNullException("Должность не может быть пустой.");
                }
                else
                {
                    _position = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.position)));
                }
            }
            get => _position;
        }
        /// <summary>
        /// День рождения
        /// </summary>
        protected DateTime _birthday;
        public DateTime birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.birthday)));
            }
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="pos">Должность</param>
        /// <param name="birthday">День родения</param>
        public Employee(string name, string surname, string pos, DateTime birthday)
        {
            this.name = name;
            this.surname = surname;
            this.position = pos;
            this.birthday = birthday;
            LastID++;
            ID = LastID;
        }
        /// <summary>
        /// Переопределенный метод преобразования в строковое представение
        /// </summary>
        /// <returns>Строковое представление объекта</returns>
        public override string ToString()
        {
            return $"{name} {surname} {position} Таб. № {ID} Дата рожд. {birthday.ToShortDateString()}";
        }
        /// <summary>
        /// Переопределенный виртуальный метод проверки эквиваленстности с объектом
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Истина, если эквивалентны, иначе ложь</returns>
        public override bool Equals(object obj)
        {
            if (obj!=null && obj is Employee)
            {
                Employee t = obj as Employee;
                return t.name.Equals(name) 
                    && t.surname.Equals(surname) 
                    && t.position.Equals(position) 
                    && t.birthday.Equals(birthday)
                    &&t.ID==ID;
            }
            else
                return false;
        }

    }
}