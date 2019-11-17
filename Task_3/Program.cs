using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Task_3
{
    class Program
    {
        static int A_Value(KeyValuePair<string, int> P)
        {
            return P.Value;
        }

        static void Main(string[] args)
        {

            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four", 4 },
                {"two", 2 },
                {"one", 1 },
                {"three", 3 },
            };

            var d1 = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });

            // а) Свернуть обращение к OrderBy с использованием лямбда-выражения$
            var d2 = dict.OrderBy(e=>e.Value);

            // б) * Развернуть обращение к OrderBy с использованием делегата Predicate<T>. ( Predicate<T> == Func<KeyValuePair<string, int>, int>)
            Func<KeyValuePair<string, int>, int> func = A_Value; //принимает KeyValuePair<string, int>, возвращает int
            var d3 = dict.OrderBy(A_Value);


            foreach (var pair in d1)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            foreach (var pair in d2)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
            foreach (var pair in d3)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.ReadKey();
        }
    }
}
