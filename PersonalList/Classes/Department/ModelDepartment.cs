using System;
using System.IO;
using System.Collections.ObjectModel;

namespace PersonalList.Classes
{
    class ModelDepartment
    {
        public ModelDepartment(string Way = "data_department.db")
        {
            currentDepartmentList = new ADepartmentList();
            currentIndex = 0;
            this.WAY = Way;
        }

        public ADepartment CurrentDepartment
        {
            get
            {
                if (CurrentIndex >= 0) { return currentDepartmentList[currentIndex]; }
                else { return null; }
            }
        }

        public void LoadData()
        {
            try
            {
                using (StreamReader sr = new StreamReader(WAY))
                {
                    while (!sr.EndOfStream)
                    {
                        string department = sr.ReadLine();
                        sr.ReadLine();
                        this.currentDepartmentList.AddDepartment(new ADepartment(department));
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Нет БД отделов");
            }
        }

        public void SaveData()
        {
            using (StreamWriter sw = new StreamWriter(WAY))
            {
                foreach (ADepartment e in currentDepartmentList)
                {
                    sw.WriteLine(e.Department);
                    sw.WriteLine();
                }
            }
        }

        #region Данные

        public ADepartmentList CurrentDepartmentList
        {
            get => this.currentDepartmentList;
            set => this.currentDepartmentList = value;
        }

        public int CurrentIndex
        {
            get => this.currentIndex;
            set => this.currentIndex = value;
        }

        ADepartmentList currentDepartmentList;
        private int currentIndex;
        private readonly string WAY;
        #endregion
    }
}
