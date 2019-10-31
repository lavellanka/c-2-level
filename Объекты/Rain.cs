using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task_1
{
    class Rain : BaseObject
    {
        public Rain(Point pos, Point dir, Size size) : base(pos, dir, size) //делаем конструктор на основе базового класса
        {
        }

        // переопределяем методы 
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
        }

        public override void Update()
        {
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.Y > Game.Height || Pos.Y < 0)
            {
                Pos.X = Game.Width / 2;
                Pos.Y = Game.Height / 2;
                Random rnd1 = new Random(DateTime.Now.Millisecond);
                Random rnd2 = new Random(DateTime.Now.Millisecond + 1);
                Dir.X = rnd1.Next(-20, 20);
                Dir.Y = rnd2.Next(-20, 20);
                try
                {
                    if (Math.Abs(Dir.X) < 10 && Math.Abs(Dir.Y) < 10)
                    {
                        throw new GameObjectException("Задана низкая скорость Rain");
                    }
                }
                catch (GameObjectException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("исправляем...");
                    Dir.X = 20;
                    Dir.Y = -20;
                }
            }

            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width || Pos.X < 0)
            {
                Pos.X = Game.Width / 2;
                Pos.Y = Game.Height / 2;
                Random rnd1 = new Random(DateTime.Now.Millisecond);
                Random rnd2 = new Random(DateTime.Now.Millisecond + 1);
                Dir.X = rnd1.Next(-20, 20);
                Dir.Y = rnd2.Next(-20, 20);
                try
                {
                    if (Math.Abs(Dir.X) < 10 && Math.Abs(Dir.Y) < 10)
                    {
                        throw new GameObjectException("Задана низкая скорость Rain");
                    }
                }
                catch (GameObjectException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("исправляем...");
                    Dir.X = 20;
                    Dir.Y = -20;
                }
            }
        }
    }
}
