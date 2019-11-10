using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    /// <summary>
    /// Пример собственного исключения
    /// </summary>
    class GameObjectException : Exception
    {
        public GameObjectException(string message) : base(message)
        {
        }
    }
}
