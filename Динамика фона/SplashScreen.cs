using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Task_1
{
    
    class SplashScreen : Game
    {
        public override void Load()
        {
            _objs = new BaseObject[80];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            _objs = new BaseObject[191];
            Random rnd1 = new Random(DateTime.Now.Millisecond);
            Random rnd2 = new Random(DateTime.Now.Millisecond + 1);

            for (int i = 0; i < 30; i++)
            {
                _objs[i] = new Square(new Point(i * 5, i * 5), new Point(20, 20), new Size(1, 1));
            }
            for (int i = 30; i < 190; i++)
            {
                _objs[i] = new Rain(new Point(Game.Width / 2, Game.Height / 2), new Point(rnd1.Next(-20, 20), rnd2.Next(-20, 20)), new Size(1, 1));
            }
            for (int i = 190; i < 191; i++)
            {
                _objs[i] = new Planet(new Point(300, 300), new Point(rnd1.Next(1, 10), rnd2.Next(1, 10)), new Size(50, 50));
            }

            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd1.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(600, rnd1.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
        }
    }
}
