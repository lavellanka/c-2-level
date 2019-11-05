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



namespace Task_1
{
    /// <summary>
    /// Класс работников с фиксированной месячной ставкой
    /// </summary>
    class TWorkerMonthlyPay:TWorkersBase
    {
        public decimal monthly_rate;

        public TWorkerMonthlyPay(string Name, decimal Monthly_rate):base(Name)
        {
            monthly_rate = Monthly_rate;
        }

        /// <summary>
        /// Метод расчета среднемесячной заработной платы
        /// </summary>
        /// <returns>среднемесячная заработная плата</returns>
        public override decimal PayRoll()
        {
            return monthly_rate;
        }
    }
}
