using System;


//  5. Добавить в пример Lesson3 обобщенный делегат

namespace Delegates_Observer
{
    public delegate void MyDelegate<T>(T obj); // сделал делегат обобщенным
    class Source
    {
        public event MyDelegate<object> Run; //определил Т как oblect
        public void Start()
        {
            Console.WriteLine("RUN");
            //if (Run != null) Run(this);
            Run?.Invoke(this); // упростил вывод делегата
        }
    }
    class Observer1 // Наблюдатель 1
    {
        public void Do(object o)
        {
            Console.WriteLine("Первый. Принял, что объект {0} побежал", o);
        }
    }
    class Observer2 // Наблюдатель 2
    {
        public void Do(object o)
        {
            Console.WriteLine("Второй. Принял, что объект {0} побежал", o);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Source s = new Source();
            Observer1 o1 = new Observer1();
            Observer2 o2 = new Observer2();
            MyDelegate<object> d1 = new MyDelegate<object>(o1.Do);
            s.Run += d1;
            s.Run += o2.Do;
            s.Start();
            s.Run -= d1;
            s.Start();

            Console.ReadKey();
        }
    }
}
