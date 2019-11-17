using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
//а) для целых чисел;
//б) * для обобщенной коллекции;
//в) * используя Linq.

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 4, 4, 5, 5, 6, 5, 5 };
            
            Count(list);
            Console.WriteLine();
            Count<int>(list);
            Console.WriteLine();

            //в)* используя Linq.
            var q = from x in list
                    group x by x into g
                    let count = g.Count()
                    select new { Value = g.Key, Count = count };
            foreach (var x in q)
            {
                Console.WriteLine("Элемент: " + x.Value + " Кол-во: " + x.Count);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Метод для целых чисел в листе
        /// </summary>
        static void Count(List<int> list)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var item in list)
            {
                if (dict.ContainsKey(item))
                {
                    dict[item]++;
                }
                else
                {
                    dict.Add(item, 1);
                }
            }
            foreach (var item in dict)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Обобщенный метод
        /// </summary>
        static void Count<T>(List<T> list)
        {
            Dictionary<T, int> dict = new Dictionary<T, int>();
            foreach (var item in list)
            {
                if (dict.ContainsKey(item))
                {
                    dict[item]++;
                }
                else
                {
                    dict.Add(item, 1);
                }
            }
            foreach (var item in dict)
            {
                Console.WriteLine(item);
            }
        }

        
    }
}
