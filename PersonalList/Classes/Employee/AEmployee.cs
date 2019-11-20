using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalList.Classes
{
    public class AEmployee : IEquatable<AEmployee>, INotifyPropertyChanged
    {
        public bool Equals(AEmployee other)
        {
            return this.surname == other.Surname
                   && this.name == other.Name
                   && this.department == other.Department;
        }

        public AEmployee(string Surname, string Name, string Department)
        {
            this.name = Name; this.surname = Surname; this.department = Department;
        }
        private AEmployee() { }

        #region Данные

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Name)));
            }
        }
        public string Surname
        {
            get
            {
                return this.surname;
            }
            set
            {
                this.surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Surname)));
            }
        }
        public string Department
        {
            get
            {
                return this.department;
            }
            set
            {
                this.department = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Department)));
            }
        }

        private string name;
        private string surname;
        private string department;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
