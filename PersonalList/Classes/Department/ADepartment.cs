using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalList.Classes
{
    public class ADepartment : IEquatable<ADepartment>, INotifyPropertyChanged
    {
        public bool Equals(ADepartment other)
        {
            return this.department == other.Department;
        }

        public ADepartment(string Departament)
        {
            this.department = Departament;
        }
        private ADepartment() { }

        #region Данные

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

        private string department;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public override string ToString()
        {
            return department;
        }
    }
}
