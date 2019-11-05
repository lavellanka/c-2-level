using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Свиргстина А.А.

//+ 1. Построить три класса(базовый и 2 потомка), описывающих некоторых работников с
//  почасовой оплатой(один из потомков) и фиксированной оплатой(второй потомок).
//+ а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной
//  платы. Для «повременщиков» формула для расчета такова: «среднемесячная заработная
//  плата = 20.8 * 8 * почасовую ставку», для работников с фиксированной оплатой
//  «среднемесячная заработная плата = фиксированной месячной оплате».
//+ b) Создать на базе абстрактного класса массив сотрудников и заполнить его.
//+ c) ** Реализовать интерфейсы для возможности сортировки массива используя Array.Sort().
//+ d) *** Создать класс содержащий массив сотрудников и реализовать возможность вывода
//  данных с использованием foreach.

//Структура программы:
//Program
//    TWorkersArray - класс с массивом сотрудников
//        TWorkersBase - базовый класс сотрудников
//            TWorkerMonthlyPay - класс сотрудников с помесячной оплатой
//            TWorkerHourlyPay - класс сотрудников с почасовой оплатой


namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10;            
            TWorkersArray list = new TWorkersArray(n); //

            // список до сортировки
            foreach (TWorkersBase item in list)
            {
                Console.WriteLine("{0, -11} {1:f2}", item.name, item.PayRoll());
            } // задание 1г: используем foreach для класса
            Array.Sort(list.Array_list);
            Console.WriteLine();
            
            // список после сортировки
            foreach (TWorkersBase item in list)
            {
                Console.WriteLine("{0, -11} {1:f2}", item.name, item.PayRoll());
            }      
            
            Console.ReadKey();
        }
    }
}
