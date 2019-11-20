using System;
using System.IO;

namespace PersonalList.Classes
{
    class ModelEmployee
    {
        public ModelEmployee(string Way = "data_employee.db")
        {
            currentEmployeeList = new AEmployeeList();
            currentIndex = 0;
            this.WAY = Way;
        }

        public AEmployee CurrentEmployee
        {
            get
            {
                if (CurrentIndex >= 0) { return currentEmployeeList[currentIndex]; }
                else { return null; }
            }
        }

        /// <summary>
        /// Загрузить данные из БД сотрудников
        /// </summary>
        public void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(WAY))
                {
                    while (!sr.EndOfStream)
                    {
                        string surname = sr.ReadLine();
                        string name = sr.ReadLine();
                        string department = sr.ReadLine();
                        sr.ReadLine();
                        this.currentEmployeeList.AddEmployee(new AEmployee(surname, name, department));
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Нет БД сотрудников");
            }
        }

        /// <summary>
        /// Сохранить данные в БД сотрудников
        /// </summary>
        public void SaveData()
        {
            using (StreamWriter sw = new StreamWriter(WAY))
            {
                foreach (AEmployee e in currentEmployeeList)
                {
                    sw.WriteLine(e.Surname);
                    sw.WriteLine(e.Name);
                    sw.WriteLine(e.Department);
                    sw.WriteLine();
                }
            }
        }

        #region Данные

        public AEmployeeList CurrentEmployeeList
        {
            get => this.currentEmployeeList;
            set => this.currentEmployeeList = value;
        }
       
        public int CurrentIndex
        {
            get => this.currentIndex;
            set => this.currentIndex = value;
        }

        AEmployeeList currentEmployeeList;
        private int currentIndex;
        private readonly string WAY;
        #endregion
    }
}
