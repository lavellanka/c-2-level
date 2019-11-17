using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task_1
{
    class Star : BaseObject //наследуем
    {
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) //делаем конструктор на основе базового класса
        {
        }

        // переопределяем методы 
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.Red, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}
