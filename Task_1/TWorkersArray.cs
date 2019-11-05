using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

//  d) * Создать класс содержащий массив сотрудников и реализовать возможность вывода
//  данных с использованием foreach.

namespace Task_1
{
    /// <summary>
    /// Класс с массивом работников
    /// </summary>
    class TWorkersArray : IEnumerable
    {
        private static TWorkersBase[] array_list;

        public TWorkersBase[] Array_list
        {
            get { return array_list; }
            set { array_list = value; }
        }

        /// <summary>
        /// Заполняем список сотрудниками
        /// </summary>
        /// <param name="n">количество сотрудников</param>
        public TWorkersArray(int n)
        {
            array_list = new TWorkersBase[n];

            //заполняем список сотрудников
            for (int i = 0; i < n; i++)
            {
                Random rnd = new Random(DateTime.Now.Millisecond + i);

                // для разнообразия сотрудников четных делаем с почасовой оплатой, нечетный - с помесячной.
                if (i % 2 == 0)
                {
                    array_list[i] = new TWorkerHourlyPay(Generator_Name(i), rnd.Next(100 + i, 500));
                }
                else
                {
                    array_list[i] = new TWorkerMonthlyPay(Generator_Name(i), rnd.Next(10000 + i, 50000));
                }
            }
        }

        /// <summary>
        /// Генератор имен
        /// </summary>
        /// <param name="i">нужен для генерации разных имен при быстром прогоне программы, чтобы рандом был разным</param>
        /// <returns>имя</returns>
        public static string Generator_Name(int i)
        {
            Random rnd = new Random(DateTime.Now.Millisecond + i);
            int z = rnd.Next(2, 10);
            char[] name_char = new char[z];
            for (int j = 0; j < z; j++)
            {
                if (j == 0)
                {
                    name_char[j] = (char)rnd.Next(0x0041, 0x005A);
                }
                else
                {
                    name_char[j] = (char)rnd.Next(0x0061, 0x007A);
                }
            }
            string name = new string(name_char);
            return name;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < array_list.Length; i++)
                yield return array_list[i];
        }
    }
}
