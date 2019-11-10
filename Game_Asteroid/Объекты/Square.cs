using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task_1
{

    class Square : BaseObject
    {
        public Square(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Yellow, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            if (Pos.Y < 0) Pos.Y = Game.Height + Size.Height;
        }
    }
}
