using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task_1
{
    class Ship : BaseObject
    {
        private int _energy = 100;
        private int _total = 0; //переменная для подсчета очков
        public int Energy => _energy;
        public int Total => _total;
        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public void EnergyUp(int n)
        {
            _energy += n;
        }
        public void TotalUp(int n)
        {
            _total += n;
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
        }
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        public void Left()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }
        public void Right()
        {
            if (Pos.X < Game.Width) Pos.X = Pos.X + Dir.X;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }
        public static event Message MessageDie;
    }
}
