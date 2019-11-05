using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  1. Построить три класса (базовый и 2 потомка), описывающих некоторых работников с
//  почасовой оплатой (один из потомков) и фиксированной оплатой (второй потомок).
//  а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной
//  платы. Для «повременщиков» формула для расчета такова: «среднемесячная заработная
//  плата = 20.8 * 8 * почасовую ставку», для работников с фиксированной оплатой
//  «среднемесячная заработная плата = фиксированной месячной оплате».
//  с) * Реализовать интерфейсы для возможности сортировки массива используя Array.Sort().


namespace Task_1
{
    /// <summary>
    /// Базовый класс работников
    /// </summary>
    abstract class TWorkersBase : IComparable<TWorkersBase>
    {
        public string name;

        public TWorkersBase (string Name)
        {
            name = Name;
        }

        public int CompareTo(TWorkersBase obj)
        {
            return this.name.CompareTo(obj.name);
        }

        /// <summary>
        /// Абстрактный метод для расчета средней заработной платы
        /// </summary>
        public abstract decimal PayRoll();
    }
}
